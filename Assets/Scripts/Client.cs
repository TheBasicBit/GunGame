using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BlindDeer.Network.Client
{
    public class Client
    {
        private readonly TcpClient _client;

        public Connection Connection { get; }

        public event EventHandler<ConnectionEventArgs> Closed;

        public event EventHandler<ConnectionPacketOutputEventArgs> Output;

        public Client(IPAddress address, int port)
        {
            _client = new TcpClient(address.ToString(), port);
            Connection = new Connection(_client);
            Connection.Closed += OnConnectionClosed;
            Connection.Output += OnConnectionPacketInput;
        }

        public bool SendPacket(Packet packet)
        {
            return Connection.SendPacket(packet);
        }

        private void OnConnectionPacketInput(object sender, ConnectionPacketOutputEventArgs e)
        {
            Output?.Invoke(this, new ConnectionPacketOutputEventArgs(e.Connection, e.Packet));
        }

        private void OnConnectionClosed(object sender, ConnectionEventArgs e)
        {
            Closed?.Invoke(this, new ConnectionEventArgs(e.Connection));
        }
    }
}