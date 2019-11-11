using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BaseSystem.Network
{
    public static class PacketUtils
    {
        public static dynamic CreatePacket(string name, Dictionary<string, string> fields)
        {
            Type type = GetPacketType(name);
            object obj = Activator.CreateInstance(type);

            foreach (string field in fields.Keys)
            {
                Type fieldType = GetFieldType(type, field);
                type.GetField(field).SetValue(obj, StringToObject(fieldType, fields[field]));
            }

            return obj;
        }

        public static Type GetPacketType(string name)
        {
            return Type.GetType("BaseSystem.Network.Packets." + name);
        }

        public static Type GetFieldType(Type type, string field)
        {
            return type.GetField(field).FieldType;
        }

        public static object StringToObject(Type type, string str)
        {
            if (type == typeof(string))
            {
                return str;
            }
            else
            {
                return type.GetMethod("Parse").Invoke(null, new object[] { str });
            }
        }
    }
}