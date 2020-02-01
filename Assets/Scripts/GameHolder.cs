using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BlindDeer.GameBase
{
    public class GameHolder : MonoBehaviour
    {
        public void Start()
        {
            BaseGameSystem.Main();
        }

        public void Update()
        {
            BaseGameSystem.OnEngineUpdate();
        }

        public static GameObject CreateObject(GameObject gameObject, Vector3 pos, Vector3 rot)
        {
            return Instantiate(gameObject, pos, Quaternion.Euler(rot));
        }
    }
}