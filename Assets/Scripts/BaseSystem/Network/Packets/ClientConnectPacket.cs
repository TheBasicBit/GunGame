using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.BaseSystem.Network.Packets
{
    public struct ClientConnectPacket
    {
        public int ClientId { get; }

        public float PosX { get; }
        public float PosY { get; }
        public float PosZ { get; }

        public float RotX { get; }
        public float RotY { get; }
        public float RotZ { get; }
    }
}