using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BlindDeer.GameBase
{
    public class FPSLimiter : MonoBehaviour
    {
        public int fpsLimit = 60;

        public void Start()
        {
            Application.targetFrameRate = fpsLimit;
        }
    }
}