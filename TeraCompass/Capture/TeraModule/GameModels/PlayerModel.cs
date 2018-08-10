﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TeraCompass.Tera.Core;
using TeraCompass.Tera.Core.Game;

using Point = System.Drawing.Point;

namespace TeraCompass.GameModels
{
    public class PlayerModel
    {
        public EntityId EntityId { get; set; }

        public Vector3f Position { get; set; }
        public RelationType Relation { get; set; }
        public string Name { get; set; }
        public string GuildName { get; set; }
        public Vector2 ScreenPosition { get; set; }
        public PlayerClass PlayerClass { get; set; }
        public PlayerModel(UserEntity obj)
        {
            Relation = obj.Relation;
            EntityId = obj.Id;
            Position = obj.Position;
            Name = obj.Name;
            GuildName = obj.GuildName;
            PlayerClass = obj.RaceGenderClass.Class;
        }
    }
}
