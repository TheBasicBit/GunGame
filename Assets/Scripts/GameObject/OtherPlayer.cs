using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayer : MonoBehaviour
{
    public static Dictionary<ulong, OtherPlayer> OtherPlayers { get; } = new Dictionary<ulong, OtherPlayer>();

    public ulong id;

    public void Start()
    {
        OtherPlayers.Add(id, this);
    }

    public void Destroy()
    {
        GameSystem.DestroyObject(this);
        OtherPlayers.Remove(id);
    }
}