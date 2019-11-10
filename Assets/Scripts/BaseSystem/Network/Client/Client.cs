using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Threading;
using BaseSystem.Network.Packets;
using System.Diagnostics;
using Assets.Scripts.BaseSystem.Network;

namespace BaseSystem.Network.Client
{
    public class Client : ClientBase, IDisposable
    {
        private readonly Stopwatch keepAliveTimer = new Stopwatch();

        private bool disposed = false;

        public event EventHandler<IncomingPacketEventArgs> IncomingPacket = (object sender, IncomingPacketEventArgs e) => { };

        public Thread UpdateLoopTask { get; }

        public Client(string ip, int port) : base(ip, port, 5000)
        {
            keepAliveTimer.Start();
            (UpdateLoopTask = new Thread(new ThreadStart(UpdateLoop))).Start();
        }

        private void UpdateLoop()
        {
            while (!disposed)
            {
                if (CanRead)
                {
                    IncomingPacket.Invoke(this, new IncomingPacketEventArgs(ReceivePacket()));
                }

                if (keepAliveTimer.ElapsedMilliseconds > 1000)
                {
                    keepAliveTimer.Restart();
                    SendPacket(new KeepAlivePacket());
                }
            }
        }

        public void Dispose()
        {
            disposed = true;
        }
    }
}