﻿using System.Text;
using TeraCompass.Tera.Core.Game.Services;

namespace TeraCompass.Tera.Core.Game.Messages.Server
{
    public class SpawnUserServerMessage : ParsedMessage
    {
        internal SpawnUserServerMessage(TeraMessageReader reader)
            : base(reader)
        {
            reader.Skip(8);
            var nameOffset = reader.ReadUInt16();
            reader.Skip(14);
            ServerId = reader.ReadUInt32();
            PlayerId = reader.ReadUInt32();
            Id = reader.ReadEntityId();
            Position = reader.ReadVector3f();
            Heading = reader.ReadAngle();
            Relation =(RelationType) reader.ReadInt32();
            RaceGenderClass = new RaceGenderClass(reader.ReadInt32());
            reader.Skip(11);
            Dead = reader.ReadByte() == 0;
            reader.Skip(121);
            Level = reader.ReadInt16();
            reader.BaseStream.Position=nameOffset-4;
            Name = reader.ReadTeraString();
            GuildName = reader.ReadTeraString();
            //Trace.WriteLine(Name + ":" + BitConverter.ToString(BitConverter.GetBytes(Id.Id))+ ":"+ ServerId.ToString()+" "+ BitConverter.ToString(BitConverter.GetBytes(PlayerId))+" "+Dead);
        }
        public RelationType Relation { get; set; }
        public int Level { get; private set; }
        public bool Dead { get; set; }
        public Angle Heading { get; set; }
        public Vector3f Position { get; set; }
        public EntityId Id { get; private set; }
        public uint ServerId { get; private set; }
        public uint PlayerId { get; private set; }
        public string Name { get; private set; }
        public string GuildName { get; private set; }
        public RaceGenderClass RaceGenderClass { get; }
    }
}