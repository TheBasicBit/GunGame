﻿using BaseSystem.Network.Client;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using BaseSystem;
using BaseSystem.Network.Packets;
using BaseSystem.Network;

public static class GameSystem
{
    public static SystemHolder SystemHolder { get; private set; }
    public static Player Player { get => SystemHolder.player.GetComponent<Player>(); }
    public static PlayerCamera PlayerCamera { get => SystemHolder.playerCamera.GetComponent<PlayerCamera>(); }
    public static Client Client { get; } = new Client(Config.ServerAddress, Config.ServerPort);
    public static List<Action> SyncActions { get; } = new List<Action>();
    public static Vector3 LastPlayerPos { get; private set; } = new Vector3(0, 0, 0);

    public static Stopwatch KeepAliveTimer { get; } = new Stopwatch();
    public static Stopwatch PositionTimer { get; } = new Stopwatch();

    public static void Main(SystemHolder systemHolder)
    {
        SystemHolder = systemHolder;

        Client.IncomingPacket += Client_IncomingPacket;
        KeepAliveTimer.Start();
    }

    public static void OnExit()
    {
        Client.SendPacket(new DisconnectPacket());
        Client.Dispose();
    }

    private static void Client_IncomingPacket(object sender, IncomingPacketEventArgs e)
    {
        NetworkManager.OnPacket(e.Packet);
    }

    public static void OnTick()
    {
        while (SyncActions.Count != 0)
        {
            SyncActions.Last().Invoke();
            SyncActions.RemoveAt(SyncActions.Count - 1);
        }

        if (KeepAliveTimer.ElapsedMilliseconds > 1000)
        {
            KeepAliveTimer.Restart();
            Client.SendPacket(new KeepAlivePacket());
        }

        Vector3 pos = Player.transform.position;

        if (PositionTimer.ElapsedMilliseconds > 333 && LastPlayerPos != pos)
        {
            PositionTimer.Restart();
            Client.SendPacket(new PositionPacket() { x = pos.x, y = pos.y, z = pos.z });
        }

        LastPlayerPos = Player.transform.position;
    }

    public static void RunSync(Action action)
    {
        SyncActions.Add(action);
    }

    public static GameObject CreateObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return SystemHolder.CreateObject(prefab, position, rotation);
    }

    public static void DestroyObject(GameObject gameObject)
    {
        SystemHolder.DestroyObject(gameObject);
    }

    public static void DestroyObject(MonoBehaviour script)
    {
        DestroyObject(script.gameObject);
    }

    public static void SpawnPlayer(int id, Vector3 position, Vector3 rotation)
    {
        CreateObject(SystemHolder.otherPlayer, position, Quaternion.Euler(rotation)).GetComponent<OtherPlayer>().id = id;
    }
}