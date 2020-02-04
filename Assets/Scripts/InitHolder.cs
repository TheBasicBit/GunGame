using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BlindDeer.GameBase
{
    public class InitHolder : MonoBehaviour
    {
        public void Awake()
        {
            BaseGameSystem.Main();
        }
    }
}