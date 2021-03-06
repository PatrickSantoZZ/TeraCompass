﻿using System.Diagnostics;
using TeraCompass.Tera.Core.Game.Services;

namespace TeraCompass.Tera.Core.Game.Messages.Server
{
    public class S_VISIT_NEW_SECTION : ParsedMessage
    {
        internal S_VISIT_NEW_SECTION(TeraMessageReader reader)
            : base(reader)
        {
            IsFirsVisit = reader.ReadBoolean();
            MapId = reader.ReadUInt32();
            GuardId = reader.ReadUInt32();
            SectionId = reader.ReadUInt32();
        }

        public bool IsFirsVisit { get; set; }
        public uint MapId { get; set; }
        public uint GuardId { get; set; }
        public uint SectionId { get; set; }
    }
}