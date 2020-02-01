using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BlindDeer.Game.PiouPiou
{
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
            Destroy(this);
            OtherPlayers.Remove(id);
        }

        public void MoveTo(Vector3 pos, Vector3 rot, float seconds)
        {
            GetComponent<InterpolateMovementScript>().MoveTo(pos, rot, seconds);
        }
    }
}