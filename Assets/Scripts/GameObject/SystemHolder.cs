using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemHolder : MonoBehaviour
{
    public GameObject player;
    public GameObject playerCamera;
    public GameObject otherPlayer;

    public void Start()
    {
        GameSystem.Main(this);
    }

    public void Update()
    {
        GameSystem.OnTick();
    }

    public GameObject CreateObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return Instantiate(prefab, position, rotation);
    }

    public void DestroyObject(GameObject gameObject)
    {
        Destroy(gameObject);
    }

    public void OnApplicationQuit()
    {
        GameSystem.OnExit();
    }
}