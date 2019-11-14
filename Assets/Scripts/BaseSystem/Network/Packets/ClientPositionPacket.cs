using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSystem.Network.Packets
{
    public struct ClientPositionPacket
    {
        public int clientId;
        public float yaw;
        public float pitch;
        public float x;
        public float y;
        public float z;
    }
}