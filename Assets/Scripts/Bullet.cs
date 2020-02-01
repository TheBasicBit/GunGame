using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BlindDeer.Game.PiouPiou
{
    public class Bullet : MonoBehaviour
    {
        public float power;
        public float maxFlyWide;

        public bool _____________________________________;

        public Vector3 startPosition;
        public ulong shooterId;

        public void Start()
        {
            GetComponent<Rigidbody>().AddForce(transform.TransformDirection(new Vector3(0, 0, power * 1000)), ForceMode.Force);
        }

        public void Update()
        {
            if (Vector3.Distance(startPosition, transform.position) > maxFlyWide)
            {
                Destroy(gameObject);
            }
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == 10)
            {
                Destroy(gameObject);
            }
        }
    }
}