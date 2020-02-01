using BlindDeer.GameBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BlindDeer.Game.PiouPiou
{
    public class PiouPiouHolder : MonoBehaviour
    {
        public GameObject player;
        public GameObject otherPlayer;
        public GameObject playerCamera;
        public GameObject bullet;

        public void OnEnable()
        {
            BaseGameSystem.Game = new PiouPiouSystem()
            {
                Player = player.GetComponent<Player>(),
                OtherPlayerPrefab = otherPlayer,
                PlayerCamera = playerCamera.GetComponent<PlayerCamera>(),
                BulletPrefab = bullet
            };
        }
    }
}