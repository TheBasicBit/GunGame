using System;
using System.Collections.Generic;
using System.Text;

namespace BlindDeer.Network
{
    public enum PacketField
    {
        GameType = 0,
        ServerLogMessage = 1,
        ServerLogType = 2,
        PlayerPosX = 3,
        PlayerPosY = 4,
        PlayerPosZ = 5,
        PlayerId = 6,
        ShootPosX = 7,
        ShootPosY = 8,
        ShootPosZ = 9,
        ShootRotX = 10,
        ShootRotY = 11,
        ShootRotZ = 12,
        YourId = 13,
        PacketType = 14
    }
}