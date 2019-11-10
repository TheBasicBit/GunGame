﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSystem.Network.Packets
{
    public struct ClientDisconnectPacket
    {
        public ulong clientId;

        public ClientDisconnectPacket(ulong id)
        {
            clientId = id;
        }
    }
}