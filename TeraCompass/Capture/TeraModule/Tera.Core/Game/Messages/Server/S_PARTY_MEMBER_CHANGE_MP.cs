﻿using TeraCompass.Tera.Core.Game.Services;

namespace TeraCompass.Tera.Core.Game.Messages.Server
{
    public class SPartyMemberChangeMp : ParsedMessage
    {
        internal SPartyMemberChangeMp(TeraMessageReader reader) : base(reader)
        {
            ServerId = reader.ReadUInt32();
            PlayerId = reader.ReadUInt32();
            MpRemaining = reader.ReadInt32();
            TotalMp = reader.ReadInt32();
            Unknow3 = reader.ReadInt16();
            //   Trace.WriteLine("target = " + TargetId + ";Mp left:" + MpRemaining + ";Max MP:" + TotalMp+";Unknow3:"+Unknow3);
        }

        public int Unknow3 { get; }

        public int MpRemaining { get; }

        public int TotalMp { get; }


        public uint ServerId { get; }
        public uint PlayerId { get; }
    }
}