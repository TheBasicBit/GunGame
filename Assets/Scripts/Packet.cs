using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Net.Sockets;

namespace BlindDeer.Network
{
    public class Packet
    {
        private readonly Dictionary<int, object> _data = new Dictionary<int, object>();

        public Packet()
        {
        }

        internal Packet(byte[] bytes)
        {
            _data = (Dictionary<int, object>)ByteArrayToObject(bytes);
        }

        public object this[PacketField field]
        {
            get
            {
                return _data.ContainsKey((int)field) ? _data[(int)field] : null;
            }

            set
            {
                _data[(int)field] = value;
            }
        }

        public bool Contains(PacketField field)
        {
            return _data.ContainsKey((int)field);
        }

        internal byte[] GetBytes()
        {
            return ObjectToByteArray(_data);
        }

        private static byte[] ObjectToByteArray(object obj)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, obj);
            byte[] bytes = ms.ToArray();
            ms.Dispose();
            return bytes;
        }

        private static object ByteArrayToObject(byte[] bytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            memStream.Write(bytes, 0, bytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            object obj = bf.Deserialize(memStream);
            memStream.Dispose();
            return obj;
        }
    }
}