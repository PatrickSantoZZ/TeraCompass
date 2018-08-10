﻿using TeraCompass.Tera.Core.Game.Services;

namespace TeraCompass.Tera.Core.Game.Messages.Server
{
    public class SUserStatus : ParsedMessage
    {
        internal SUserStatus(TeraMessageReader reader) : base(reader)
        {
            User = reader.ReadEntityId();
            Status = reader.ReadByte();
        }

        public EntityId User { get; }
        public byte Status { get; } //0=idle,1=combat,2=camp fire
    }
}