using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Threading;
using BaseSystem.Network.Packets;
using System.Diagnostics;

namespace BaseSystem.Network.Client
{
    public class Client : ClientBase, IDisposable
    {
        private bool disposed = false;

        public event EventHandler<IncomingPacketEventArgs> IncomingPacket = (object sender, IncomingPacketEventArgs e) => { };

        public Thread UpdateLoopTask { get; }

        public Client(string ip, int port) : base(ip, port)
        {
            (UpdateLoopTask = new Thread(new ThreadStart(UpdateLoop))).Start();
        }

        private void UpdateLoop()
        {
            while (!disposed)
            {
                IncomingPacket.Invoke(this, new IncomingPacketEventArgs(ReceivePacket()));
            }
        }

        public void Dispose()
        {
            disposed = true;
        }
    }
}