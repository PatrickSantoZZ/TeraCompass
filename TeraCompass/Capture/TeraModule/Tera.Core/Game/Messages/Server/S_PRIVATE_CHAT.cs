﻿using System.Diagnostics;
using TeraCompass.Tera.Core.Game.Services;

namespace TeraCompass.Tera.Core.Game.Messages.Server
{
    public class S_PRIVATE_CHAT : ParsedMessage
    {
        internal S_PRIVATE_CHAT(TeraMessageReader reader) : base(reader)
        {
            AuthorNameOffset = reader.ReadUInt16();
            TextOffset = reader.ReadUInt16();
            Channel = reader.ReadInt32();
            AuthorId = reader.ReadUInt64();
            AuthorName = reader.ReadTeraString();
            Text = reader.ReadTeraString();
            Trace.WriteLine("Channel:"+Channel+";Username:"+AuthorName+";Text:"+Text+";AuthorId:"+AuthorId);
        }
        public ushort AuthorNameOffset { get; set; }
        public ushort TextOffset { get; set; }
        public string AuthorName { get; set; }

        public ulong AuthorId { get; set; }

        public string Text { get; set; }

        public int Channel { get; set; }
    }
}