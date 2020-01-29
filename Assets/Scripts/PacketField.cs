using System;
using System.Collections.Generic;
using System.Text;

namespace BlindDeer.Network
{
    public enum PacketField
    {
        GameType = 0x00000000,
        ServerLogMessage = 0x00000001,
        ServerLogType = 0x00000002
    }
}