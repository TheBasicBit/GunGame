using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;
using System.Threading;
using BaseSystem.Network.Packets;

namespace BaseSystem.Network.Server
{
    public class Server : IDisposable
    {
        private const string DisposedObjectName = "Server";

        private readonly List<ServerSideClient> clients = new List<ServerSideClient>();

        private readonly TcpListener listener;
        private bool listening = false;
        private bool disposed = false;

        public IDManager PlayerIDManager { get; } = new IDManager();
        public Thread UpdateLoopTask { get; }
        public TimeSpan TimeOut { get; }
        public int Port { get; }

        public Server(int port, TimeSpan timeOut)
        {
            TimeOut = timeOut;
            listener = new TcpListener(IPAddress.Any, Port = port);
            Listening = true;

            (UpdateLoopTask = new Thread(new ThreadStart(UpdateLoop))).Start();
        }

        public bool Listening
        {
            get
            {
                if (disposed)
                {
                    throw new ObjectDisposedException(DisposedObjectName);
                }

                return listening;
            }

            set
            {
                if (disposed)
                {
                    throw new ObjectDisposedException(DisposedObjectName);
                }

                listening = value;

                if (listening)
                {
                    listener.Start();
                }
                else
                {
                    listener.Stop();
                }
            }
        }

        public ServerSideClient[] Clients
        {
            get
            {
                if (disposed)
                {
                    throw new ObjectDisposedException(DisposedObjectName);
                }

                return clients.ToArray();
            }
        }

        public void Dispose()
        {
            Listening = false;

            foreach (ServerSideClient client in clients)
            {
                client.Disconnect("ServerClosed");
            }

            disposed = true;
        }

        public void DestroyClient(ServerSideClient client)
        {
            if (clients.Contains(client))
            {
                clients.Remove(client);
            }
        }

        public ServerSideClient GetClientById(int id)
        {
            foreach (ServerSideClient serverSideClient in Clients)
            {
                if (serverSideClient.Id == id)
                {
                    return serverSideClient;
                }
            }

            return null;
        }

        private void UpdateLoop()
        {
            try
            {
                while (!disposed)
                {
                    if (Listening && listener.Pending())
                    {
                        clients.Add(new ServerSideClient(this, listener.AcceptTcpClient()));
                    }

                    foreach (ServerSideClient client in Clients)
                    {
                        if (disposed)
                        {
                            break;
                        }
                        else if (client.TimeAfterLastKeepAlive >= new TimeSpan(0, 0, 3))
                        {
                            client.Disconnect("TimedOut");
                            clients.Remove(client);
                            continue;
                        }
                        else if (client.CanRead)
                        {
                            object packet;

                            try
                            {
                                packet = client.ReceivePacket();
                            }
                            catch (IOException)
                            {
                                client.Disconnect("CanNotReadPacket");
                                continue;
                            }

                            client.OnIncomingPacket(packet);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}