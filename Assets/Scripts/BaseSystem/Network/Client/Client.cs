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
    public class Client : ClientBase
    {
        private readonly Stopwatch keepAliveTimer = new Stopwatch();

        public Thread UpdateLoopTask { get; }

        public Client(string ip, int port) : base(ip, port, 5000)
        {
            keepAliveTimer.Start();
            (UpdateLoopTask = new Thread(new ThreadStart(UpdateLoop))).Start();
        }

        private void UpdateLoop()
        {
            try
            {
                while (true)
                {
                    if (CanRead)
                    {
                        object packet = ReceivePacket();

                        if (packet is ChatMessagePacket chatMessagePacket)
                        {
                            Console.WriteLine(chatMessagePacket.message);
                        }
                    }

                    if (keepAliveTimer.ElapsedMilliseconds > 1000)
                    {
                        keepAliveTimer.Restart();
                        SendPacket(new KeepAlivePacket());
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
        }
    }
}