using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSystem.Network.Packets
{
    public struct BulletPositionPacket
    {
        public int bulletId;
        public float x;
        public float y;
        public float z;
    }
}