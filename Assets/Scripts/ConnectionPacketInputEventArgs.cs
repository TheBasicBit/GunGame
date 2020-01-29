using System;
using System.Collections.Generic;
using System.Text;

namespace BlindDeer.Network
{
    public class ConnectionPacketOutputEventArgs : EventArgs
    {
        public Connection Connection { get; }

        public Packet Packet { get; }

        public ConnectionPacketOutputEventArgs(Connection connection, Packet packet)
        {
            Connection = connection;
            Packet = packet;
        }
    }
}