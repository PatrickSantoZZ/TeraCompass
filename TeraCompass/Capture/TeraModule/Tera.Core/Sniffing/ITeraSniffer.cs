﻿// Copyright (c) Gothos
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using TeraCompass.Tera.Core.Game;

namespace TeraCompass.Tera.Core.Sniffing
{
    public interface ITeraSniffer
    {
        bool Enabled { get; set; }
        event Action<Message> MessageReceived;
        event Action<Server> NewConnection;
    }
}