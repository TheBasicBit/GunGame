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
        public void OnEnable()
        {
            BaseGameSystem.Game = new PiouPiouSystem();
        }
    }
}