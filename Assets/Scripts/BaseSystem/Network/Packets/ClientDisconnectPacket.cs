using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSystem.Network.Packets
{
    public struct ClientDisconnectPacket
    {
        public int clientId;

        public ClientDisconnectPacket(int id)
        {
            clientId = id;
        }
    }
}