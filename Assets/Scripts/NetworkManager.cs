using BaseSystem.Network.Packets;
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
            GameSystem.RunSync(new Action(() =>
            {
                GameSystem.SpawnPlayer(clientConnectPacket.clientId, new Vector3(clientConnectPacket.posX, clientConnectPacket.posY, clientConnectPacket.posZ), new Vector3(clientConnectPacket.rotX, clientConnectPacket.rotY, clientConnectPacket.rotZ));
            }));
        }
        else if (packet is ClientDisconnectPacket clientDisconnectPacket)
        {
        }
    }
}