using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using UnityEngine;

using Console = UnityEngine.Debug;

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
            Client.Power();
            ClientKeepAlive.Restart();
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

public class Packet
{
    private readonly byte[] buffer = new byte[128];

    public static implicit operator byte[](Packet packet)
    {
        return packet.buffer;
    }

    public ushort ID
    {
        get
        {
            return BitConverter.ToUInt16(new byte[] { buffer[0], buffer[1] }, 0);
        }

        set
        {
            byte[] id = BitConverter.GetBytes(value);
            buffer[0] = id[0];
            buffer[1] = id[1];
        }
    }
}

public static class Client
{
    private static readonly TcpClient client = new TcpClient();

    public static bool Connect(string host)
    {
        try
        {
            string address = host.Split(':')[0];
            string port = host.Split(':')[1];

            client.Connect(new IPEndPoint(IPAddress.Parse(address), int.Parse(port)));

            return true;
        }
        catch
        {
            return false;
        }
    }

    public static void Power()
    {
        Console.Log("Client: KeepAlive");
    }
}