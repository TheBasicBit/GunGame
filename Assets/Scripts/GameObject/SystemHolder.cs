using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemHolder : MonoBehaviour
{
    public void Start()
    {
        GameSystem.Main();
    }

    public void Update()
    {
        GameSystem.OnTick();
    }
}