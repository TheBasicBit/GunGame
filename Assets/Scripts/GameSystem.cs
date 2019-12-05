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
using Debug = UnityEngine.Debug;
using UnityEngine.SceneManagement;

public static class GameSystem
{
    public static SystemHolder SystemHolder { get; private set; }
    public static Player Player { get => SystemHolder.player.GetComponent<Player>(); }
    public static PlayerCamera PlayerCamera { get => SystemHolder.playerCamera.GetComponent<PlayerCamera>(); }
    public static Client Client { get; } = new Client(Config.ServerAddress, Config.ServerPort);
    public static List<Action> SyncActions { get; } = new List<Action>();
    public static List<Action> LateSyncActions { get; } = new List<Action>();

    public static Stopwatch KeepAliveTimer { get; } = new Stopwatch();
    public static Stopwatch PositionTimer { get; } = new Stopwatch();

    public static void Main(SystemHolder systemHolder)
    {
        SystemHolder = systemHolder;

        Client.IncomingPacket += Client_IncomingPacket;
        KeepAliveTimer.Start();
        PositionTimer.Start();
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

        if (PositionTimer.ElapsedMilliseconds > 100)
        {
            PositionTimer.Restart();
            Client.SendPacket(new PositionPacket() { posX = pos.x, posY = pos.y, posZ = pos.z, rotX = PlayerCamera.transform.eulerAngles.x, rotY = PlayerCamera.transform.eulerAngles.y, rotZ = PlayerCamera.transform.eulerAngles.z });
        }
    }

    public static void OnLateTick()
    {
        while (LateSyncActions.Count != 0)
        {
            LateSyncActions.Last().Invoke();
            LateSyncActions.RemoveAt(LateSyncActions.Count - 1);
        }
    }

    public static void RunSync(Action action)
    {
        SyncActions.Add(action);
    }

    public static void RunLateSync(Action action)
    {
        LateSyncActions.Add(action);
    }

    public static GameObject CreateObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return SystemHolder.CreateObject(prefab, position, rotation);
    }

    public static GameObject CreateObject(this GameObject parent, GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return SystemHolder.CreateObject(parent, prefab, position, rotation);
    }

    public static void DestroyObject(GameObject gameObject)
    {
        SystemHolder.DestroyObject(gameObject);
    }

    public static void Destroy(this GameObject gameObject)
    {
        DestroyObject(gameObject);
    }

    public static Vector3 ToAngle(this Vector3 vector)
    {
        return new Vector3(vector.x % 360f, vector.y % 360f, vector.z % 360f);
    }

    public static void DestroyObject(MonoBehaviour script)
    {
        DestroyObject(script.gameObject);
    }

    public static void SpawnPlayer(int id, Vector3 position, Vector3 rotation)
    {
        SystemHolder.playerContainer.CreateObject(SystemHolder.otherPlayer, position, Quaternion.Euler(rotation)).GetComponent<OtherPlayer>().id = id;
    }

    public static GameObject GetChildWithName(this MonoBehaviour obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }

    public static bool HasComponent<T>(this GameObject obj)
    {
        return obj.GetComponent<T>() != null;
    }
}