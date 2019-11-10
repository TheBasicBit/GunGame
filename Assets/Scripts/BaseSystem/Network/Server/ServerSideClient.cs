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
        private bool disconnected = false;

        public Server Server { get; }
        public int Id { get; }
        public TimeSpan TimeAfterLastKeepAlive { get => keepAliveTimer.Elapsed; }
        public IPAddress Address { get; }

        public ServerSideClient(Server server, TcpClient client) : base(client, (int)server.TimeOut.TotalMilliseconds)
        {
            Server = server;
            Id = server.PlayerIDManager.CreateNewID();
            Address = ((IPEndPoint)client.Client.RemoteEndPoint).Address;

            Console.WriteLine(this + " connected.");

            foreach (ServerSideClient otherClient in Server.Clients)
            {
                if (this != otherClient)
                {
                    otherClient.SendPacket(new ClientConnectPacket()
                    {
                        clientId = Id,
                        posX = 0,
                        posY = 0,
                        posZ = 0,
                        rotX = 0,
                        rotY = 0,
                        rotZ = 0
                    });

                    SendPacket(new ClientConnectPacket()
                    {
                        clientId = Id,
                        posX = 0,
                        posY = 0,
                        posZ = 0,
                        rotX = 0,
                        rotY = 0,
                        rotZ = 0
                    });
                }
            }
            keepAliveTimer.Start();
        }

        public override string ToString()
        {
            return "Client[Address: " + Address + ", ID: " + Id + "]";
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
            else if (packet is DisconnectPacket)
            {
                Disconnect("DisconnectPacket");
            }
        }

        public void Disconnect(string reason)
        {
            if (!disconnected)
            {
                Server.DestroyClient(this);
                Console.WriteLine(this + " disconnected. (" + reason + ")");
                tcpClient.Close();
                disconnected = true;
            }
        }
    }
}