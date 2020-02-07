using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BlindDeer.Game.PiouPiou
{
    public class CameraSideRotation : MonoBehaviour
    {
        public float speed = 1;

        public void Update()
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + (Time.deltaTime * speed), transform.eulerAngles.z);
        }
    }
}