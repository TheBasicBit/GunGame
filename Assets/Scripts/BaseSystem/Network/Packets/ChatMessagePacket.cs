using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace BaseSystem.Network.Packets
{
    public struct ChatMessagePacket
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string message;
    }
}