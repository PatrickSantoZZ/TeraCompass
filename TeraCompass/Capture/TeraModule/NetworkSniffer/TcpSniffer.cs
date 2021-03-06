﻿using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using PacketDotNet;

namespace TeraCompass.NetworkSniffer
{
    public class TcpSniffer
    {
        private readonly ConcurrentDictionary<ConnectionId, TcpConnection> _connections = new ConcurrentDictionary<ConnectionId, TcpConnection>();

        private readonly object _lock = new object();

        private readonly string SnifferType;
        public TcpSniffer(IpSniffer ipSniffer)
        {
            ipSniffer.PacketReceived += Receive;
            SnifferType = ipSniffer.GetType().FullName;
        }

        public string TcpLogFile { get; set; }

        public event Action<TcpConnection> NewConnection;
        public event Action<TcpConnection> EndConnection;

        protected void OnNewConnection(TcpConnection connection)
        {
            var handler = NewConnection;
            handler?.Invoke(connection);
        }

        protected void OnEndConnection(TcpConnection connection)
        {
            var handler = EndConnection;
            handler?.Invoke(connection);
        }

        internal void RemoveConnection(TcpConnection connection)
        {
            if (!_connections.ContainsKey(connection.ConnectionId)) { return; }
            _connections.TryRemove(connection.ConnectionId, out TcpConnection temp);
            var reverse = connection.ConnectionId.Reverse;
            if (!_connections.ContainsKey(reverse)) { return; }
            _connections.TryRemove(reverse, out TcpConnection temp1);
        }

        private void Receive(IPv4Packet ipData)
        {
            var tcpPacket = ipData.PayloadPacket as TcpPacket;
            if (tcpPacket == null || tcpPacket.DataOffset * 4 > ipData.PayloadLength) { return; }
            //if (tcpPacket.Checksum!=0 && !tcpPacket.ValidTCPChecksum) return;
            var isFirstPacket = tcpPacket.Syn;
            var connectionId = new ConnectionId(ipData.SourceAddress, tcpPacket.SourcePort, ipData.DestinationAddress, tcpPacket.DestinationPort);


            TcpConnection connection;
            bool isInterestingConnection;
            if (isFirstPacket)
            {
                connection = new TcpConnection(connectionId, tcpPacket.SequenceNumber, RemoveConnection, SnifferType);
                OnNewConnection(connection);
                isInterestingConnection = connection.HasSubscribers;
                if (!isInterestingConnection) { return; }
                _connections[connectionId] = connection;
                Trace.Assert(tcpPacket.PayloadData.Length == 0);
            }
            else
            {
                isInterestingConnection = _connections.TryGetValue(connectionId, out connection);
                if (!isInterestingConnection) { return; }
                byte[] payload;
                try { payload = tcpPacket.PayloadData; }
                catch { return; }
                //_buffer.Enqueue(new QPacket(connection, tcpPacket.SequenceNumber, tcpPacket.Payload));
                lock (_lock)
                {
                    if (tcpPacket.Fin || tcpPacket.Rst)
                    {
                        OnEndConnection(connection);
                        return;
                    }
                    if(payload == null)
                    {
                        return;
                    }
                    connection.HandleTcpReceived(tcpPacket.SequenceNumber, payload);
                }
            }
        }
    }
}