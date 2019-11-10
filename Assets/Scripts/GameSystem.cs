using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public static class GameSystem
{
    public static SystemHolder SystemHolder { get; private set; }
    private static Stopwatch ClientKeepAlive { get; } = new Stopwatch();
    public static Player Player { get => SystemHolder.player.GetComponent<Player>(); }
    public static PlayerCamera PlayerCamera { get => SystemHolder.playerCamera.GetComponent<PlayerCamera>(); }

    public static void Main(SystemHolder systemHolder)
    {
        SystemHolder = systemHolder;

        ClientKeepAlive.Start();
    }

    public static void OnTick()
    {
        if (ClientKeepAlive.Elapsed.TotalSeconds >= 1)
        {
            //TODO: Send "KeepAlive"-Packet
        }
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
}