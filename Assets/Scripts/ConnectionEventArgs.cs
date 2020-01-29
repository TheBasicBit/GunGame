using System;
using System.Collections.Generic;
using System.Text;

namespace BlindDeer.Network
{
    public class ConnectionEventArgs : EventArgs
    {
        public Connection Connection { get; }

        public ConnectionEventArgs(Connection connection)
        {
            Connection = connection;
        }
    }
}