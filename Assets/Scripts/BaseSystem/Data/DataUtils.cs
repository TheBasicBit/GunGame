using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Text;
using System.Linq;

namespace BaseSystem.Data
{
    public static class DataUtils
    {
        public static string ToBase64String(this byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        public static byte[] FromBase64String(this string base64)
        {
            return Convert.FromBase64String(base64);
        }

        public static byte[] GetBytesFromStruct<T>(this T structObj) where T : struct
        {
            int size = Marshal.SizeOf(structObj);
            byte[] arr = new byte[size];
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(structObj, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);
            return arr;
        }

        public static T ToStruct<T>(this byte[] bytes) where T : struct
        {
            T str = new T();
            int size = Marshal.SizeOf(str);
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(bytes, 0, ptr, size);
            str = (T)Marshal.PtrToStructure(ptr, str.GetType());
            Marshal.FreeHGlobal(ptr);
            return str;
        }

        public static object ToStruct(this byte[] bytes, Type type)
        {
            if (!type.IsValueType)
            {
                throw new ArgumentException();
            }

            MethodInfo method = typeof(DataUtils).GetMethod("ToStruct", new Type[] { typeof(byte[]) });
            MethodInfo generic = method.MakeGenericMethod(type);
            return generic.Invoke(null, new object[] { bytes });
        }

        public static Guid ToGuid(this byte[] bytes)
        {
            if (bytes.Length != 16)
            {
                throw new ArgumentException();
            }

            return new Guid(bytes);
        }

        public static T[] SubArray<T>(this T[] objs, int startIndex, int length)
        {
            return objs.Skip(startIndex).Take(length).ToArray();
        }
    }
}