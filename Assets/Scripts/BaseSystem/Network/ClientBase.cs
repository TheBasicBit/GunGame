using BaseSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace BaseSystem.Network
{
    public class ClientBase
    {
        protected TcpClient tcpClient;

        private NetworkStream Stream { get => tcpClient.GetStream(); }

        protected ClientBase(TcpClient client)
        {
            tcpClient = client;
        }

        protected ClientBase(TcpClient client, int timeOut) : this(client)
        {
            tcpClient.SendTimeout = timeOut;
            tcpClient.ReceiveTimeout = timeOut;
        }

        protected ClientBase(string ip, int port, int timeOut)
        {
            tcpClient = new TcpClient();
            tcpClient.Connect(ip, port);
            tcpClient.SendTimeout = timeOut;
            tcpClient.ReceiveTimeout = timeOut;
        }

        protected bool SendBytes(byte[] buffer)
        {
            try
            {
                Stream.Write(BitConverter.GetBytes(buffer.Length).Concat(buffer).ToArray(), 0, 4 + buffer.Length);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CanRead
        {
            get
            {
                return tcpClient.Connected && Stream.DataAvailable;
            }
        }

        protected byte[] ReceiveBytes()
        {
            byte[] lengthBuffer = new byte[4];
            Stream.Read(lengthBuffer, 0, 4);
            int length = BitConverter.ToInt32(lengthBuffer, 0);
            byte[] buffer = new byte[length];
            Stream.Read(buffer, 0, length);
            return buffer;
        }

        public object ReceivePacket()
        {
            byte[] buffer = ReceiveBytes();
            int nameLength = BitConverter.ToInt32(buffer.Take(4).ToArray(), 0);
            Type type = Type.GetType(Encoding.ASCII.GetString(buffer.Skip(4).Take(nameLength).ToArray()));
            int structLength = BitConverter.ToInt32(buffer.Skip(4 + nameLength).Take(4).ToArray(), 0);
            return buffer.Skip(8 + nameLength).Take(structLength).ToArray().ToStruct(type);
        }

        public void SendPacket<T>(T structObj) where T : struct
        {
            byte[] nameBuffer = Encoding.ASCII.GetBytes(structObj.GetType().FullName);
            byte[] structBuffer = structObj.GetBytesFromStruct();
            SendBytes(BitConverter.GetBytes(nameBuffer.Length).Concat(nameBuffer).Concat(BitConverter.GetBytes(structBuffer.Length)).Concat(structBuffer).ToArray());
        }
    }
}