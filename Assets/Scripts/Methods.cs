using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BlindDeer.GameBase
{
    public static class Methods
    {
        public static Vector3 ToAngle(this Vector3 vector)
        {
            return new Vector3(vector.x % 360f, vector.y % 360f, vector.z % 360f);
        }
    }
}