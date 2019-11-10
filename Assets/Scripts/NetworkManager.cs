using Assets.Scripts.BaseSystem.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class NetworkManager
{
    public static void SendPacket<T>(T packet) where T : struct
    {
        GameSystem.Client.SendPacket(packet);
    }

    public static void OnPacket(object packet)
    {
        if (packet is ClientConnectPacket clientConnectPacket)
        {
            GameSystem.SpawnPlayer(clientConnectPacket.ClientId, new Vector3(clientConnectPacket.PosX, clientConnectPacket.PosY, clientConnectPacket.PosZ), new Vector3(clientConnectPacket.RotX, clientConnectPacket.RotY, clientConnectPacket.RotZ));
        }
    }
}