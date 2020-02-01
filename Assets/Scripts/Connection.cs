using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;
using System.Linq;

namespace BlindDeer.Network
{
    public class Connection : IDisposable
    {
        private readonly NetworkStream _stream;
        private readonly Stopwatch _keepAliveTimer = new Stopwatch();
        private bool disposed = false;

        public event EventHandler<ConnectionPacketOutputEventArgs> Output;

        public event EventHandler<ConnectionEventArgs> Closed;

        public TcpClient Client { get; }

        public IPAddress Address { get; }

        public Connection(TcpClient client)
        {
            Client = client;

            _stream = client.GetStream();
            _stream.ReadTimeout = _stream.WriteTimeout = 300;

            _keepAliveTimer.Start();

            Address = ((IPEndPoint)client.Client.RemoteEndPoint).Address;

            UpdateManager.Update += OnUpdate;
        }

        private void OnUpdate(object sender, EventArgs e)
        {
            try
            {
                if (_keepAliveTimer.Elapsed >= new TimeSpan(0, 0, 1))
                {
                    _keepAliveTimer.Restart();

                    KeepAlive();
                }

                if (MustRead)
                {
                    Packet packet = ReadPacket();

                    if (packet != null)
                    {
                        Output?.Invoke(this, new ConnectionPacketOutputEventArgs(this, packet));
                    }
                }
            }
            catch (ObjectDisposedException)
            {
            }
        }

        public void Dispose()
        {
            if (!disposed)
            {
                disposed = true;

                UpdateManager.Update -= OnUpdate;

                _stream.Close();
                Client.Close();

                Closed?.Invoke(this, new ConnectionEventArgs(this));
                Output = null;
                Closed = null;
            }
        }

        public bool Connected
        {
            get
            {
                return !disposed && Client.Connected;
            }
        }

        public bool SendPacket(Packet packet)
        {
            try
            {
                WriteBytes(packet.GetBytes());
                return true;
            }
            catch
            {
                Dispose();
                return false;
            }
        }

        private void KeepAlive()
        {
            try
            {
                _stream.Write(BitConverter.GetBytes(0), 0, 4);
            }
            catch
            {
                Dispose();
            }
        }

        private void WriteBytes(byte[] bytes)
        {
            List<byte> byteList = new List<byte>();
            byteList.AddRange(BitConverter.GetBytes(bytes.Length));
            byteList.AddRange(bytes);

            _stream.Write(byteList.ToArray(), 0, bytes.Length + 4);
        }

        public Packet ReadPacket()
        {
            byte[] bytes;

            try
            {
                bytes = ReadBytes();
            }
            catch
            {
                Dispose();
                return null;
            }

            if (bytes.Length == 0)
            {
                return null;
            }

            return new Packet(bytes);
        }

        private byte[] ReadBytes()
        {
            byte[] bytes = new byte[4];
            _stream.Read(bytes, 0, 4);
            int length = BitConverter.ToInt32(bytes, 0);

            if (length == 0)
            {
                return new byte[0];
            }

            bytes = new byte[length];
            _stream.Read(bytes, 0, length);

            return bytes;
        }

        private bool MustRead
        {
            get
            {
                return _stream.DataAvailable;
            }
        }
    }
}