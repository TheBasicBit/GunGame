using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayer : MonoBehaviour
{
    public static Dictionary<int, OtherPlayer> OtherPlayers { get; } = new Dictionary<int, OtherPlayer>();

    public int id;

    public void Start()
    {
        OtherPlayers.Add(id, this);
    }

    public void Destroy()
    {
        GameSystem.DestroyObject(this);
        OtherPlayers.Remove(id);
    }

    public void MoveTo(Vector3 pos, Vector3 rot, float seconds)
    {
        GetComponent<InterpolateMovementScript>().MoveTo(pos, rot, seconds);
    }
}