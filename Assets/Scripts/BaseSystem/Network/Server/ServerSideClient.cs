using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Linq;
using System.Reflection;
using BaseSystem.Data;
using System.Diagnostics;
using BaseSystem.Network.Packets;
using System.Net;

namespace BaseSystem.Network.Server
{
    public class ServerSideClient : ClientBase
    {
        private readonly Stopwatch keepAliveTimer = new Stopwatch();

        public Server Server { get; }

        public TimeSpan TimeAfterLastKeepAlive { get => keepAliveTimer.Elapsed; }

        public ServerSideClient(Server server, TcpClient client) : base(client, (int)server.TimeOut.TotalMilliseconds)
        {
            Console.WriteLine("Client[" + ((IPEndPoint)client.Client.RemoteEndPoint).Address + "] connected.");

            Server = server;
            keepAliveTimer.Start();
        }

        public void OnIncomingPacket(object packet)
        {
            if (packet is ChatMessagePacket chatMessagePacket)
            {
                foreach (ServerSideClient client in Server.Clients)
                {
                    if (client != this)
                    {
                        client.SendPacket(chatMessagePacket);
                    }
                }
            }
            else if (packet is KeepAlivePacket)
            {
                keepAliveTimer.Restart();
            }
        }

        public void ServerClosed()
        {
            Disconnected();
        }

        public void TimedOut()
        {
            Disconnected();
        }

        public void Disconnected()
        {
            Console.WriteLine("Client[" + ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address + "] disconnected.");
        }
    }
}