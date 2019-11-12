using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayer : MonoBehaviour
{
    public static Dictionary<int, OtherPlayer> OtherPlayers { get; } = new Dictionary<int, OtherPlayer>();

    public int id;

    public bool move = false;
    public float moveSeconds = 0;
    public Vector3 moveStart = new Vector3(0, 0, 0);
    public Vector3 moveEnd = new Vector3(0, 0, 0);

    public void Start()
    {
        OtherPlayers.Add(id, this);
    }

    public void Update()
    {
        if (move)
        {
            Vector3 pos = Vector3.Lerp(moveStart, moveEnd, Time.time / moveSeconds);

            transform.position = pos;

            if (moveEnd == pos)
            {
                move = false;
            }
        }
    }

    public void Destroy()
    {
        GameSystem.DestroyObject(this);
        OtherPlayers.Remove(id);
    }

    public void MoveTo(Vector3 pos, float seconds)
    {
        moveStart = transform.position;
        moveEnd = pos;
        moveSeconds = seconds;
        move = true;
    }
}