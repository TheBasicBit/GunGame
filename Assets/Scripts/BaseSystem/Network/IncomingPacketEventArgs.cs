using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.BaseSystem.Network
{
    public class IncomingPacketEventArgs : EventArgs
    {
        public object Packet { get; }

        public IncomingPacketEventArgs(object packet)
        {
            Packet = packet;
        }
    }
}