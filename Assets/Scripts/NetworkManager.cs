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
                    OtherPlayer otherPlayer = OtherPlayer.OtherPlayers[clientPositionPacket.clientId];
                    otherPlayer.MoveTo(new Vector3(clientPositionPacket.posX, clientPositionPacket.posY, clientPositionPacket.posZ), new Vector3(clientPositionPacket.rotX, clientPositionPacket.rotY, 0), 0.2f);
                }
                catch (KeyNotFoundException)
                {
                }
            }));

            GameSystem.RunLateSync(new Action(() =>
            {
                try
                {
                    OtherPlayer otherPlayer = OtherPlayer.OtherPlayers[clientPositionPacket.clientId];
                    GameObject rotBone = otherPlayer.GetChildWithName("mesh_otherPlayer/Test Rig/spine/Penis/spine.002/spine.003/RotBone");
                    rotBone.transform.eulerAngles = new Vector3(0, clientPositionPacket.rotY, 0);
                }
                catch (KeyNotFoundException)
                {
                }
            }));
        }
        else if (packet is BulletCreatePacket bulletCreatePacket)
        {
            GameSystem.RunSync(new Action(() =>
            {
                Vector3 startPosition = new Vector3(bulletCreatePacket.posX, bulletCreatePacket.posY, bulletCreatePacket.posZ);
                GameObject obj = GameSystem.CreateObject(GameSystem.Player.bulletPrefab, startPosition, Quaternion.Euler(bulletCreatePacket.rotX, bulletCreatePacket.rotY, bulletCreatePacket.rotZ));
                Bullet bullet = obj.GetComponent<Bullet>();
                bullet.startPosition = startPosition;
            }));
        }
    }
}