using Assets.Scripts.BaseSystem.Network;
using BaseSystem.Network.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public static class GameSystem
{
    public static SystemHolder SystemHolder { get; private set; }
    public static Player Player { get => SystemHolder.player.GetComponent<Player>(); }
    public static PlayerCamera PlayerCamera { get => SystemHolder.playerCamera.GetComponent<PlayerCamera>(); }
    public static Client Client { get; } = new Client("134.255.232.43", 19489);

    public static void Main(SystemHolder systemHolder)
    {
        SystemHolder = systemHolder;

        Client.IncomingPacket += Client_IncomingPacket;
    }

    private static void Client_IncomingPacket(object sender, IncomingPacketEventArgs e)
    {
        NetworkManager.OnPacket(e.Packet);
    }

    public static void OnTick()
    {
    }

    public static GameObject CreateObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return SystemHolder.CreateObject(prefab, position, rotation);
    }

    public static void Destroy(this GameObject gameObject)
    {
        SystemHolder.DestroyObject(gameObject);
    }

    public static void Destroy(this MonoBehaviour script)
    {
        script.gameObject.Destroy();
    }

    public static void SpawnPlayer(int id, Vector3 position, Vector3 rotation)
    {
        CreateObject(SystemHolder.otherPlayer, position, Quaternion.Euler(rotation)).GetComponent<OtherPlayer>().id = id;
    }
}