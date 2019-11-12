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
            Debug.Log("Client[" + clientConnectPacket.clientId + "] connected: " + clientConnectPacket.clientId);

            GameSystem.RunSync(new Action(() =>
            {
                GameSystem.SpawnPlayer(clientConnectPacket.clientId, new Vector3(clientConnectPacket.posX, clientConnectPacket.posY, clientConnectPacket.posZ), new Vector3(clientConnectPacket.rotX, clientConnectPacket.rotY, clientConnectPacket.rotZ));
            }));
        }
        else if (packet is ClientQuitPacket clientDisconnectPacket)
        {
            try
            {
                Debug.Log("Client[" + clientDisconnectPacket.clientId + "] disconnected: " + clientDisconnectPacket.clientId);

                GameSystem.RunSync(new Action(() =>
                {
                    OtherPlayer.OtherPlayers[clientDisconnectPacket.clientId].Destroy();
                }));
            }
            catch (KeyNotFoundException)
            {
            }
        }
        else if (packet is ChatMessagePacket chatMessagePacket)
        {
            Debug.Log("Chat: " + chatMessagePacket.message);
        }
        else if (packet is ClientPositionPacket clientPositionPacket)
        {
            GameSystem.RunSync(new Action(() =>
            {
                try
                {
                    OtherPlayer.OtherPlayers[clientPositionPacket.clientId].transform.position = new Vector3(clientPositionPacket.x, clientPositionPacket.y, clientPositionPacket.z);
                }
                catch
                {
                    Debug.Log(clientPositionPacket.clientId);
                }
            }));
        }
    }
}