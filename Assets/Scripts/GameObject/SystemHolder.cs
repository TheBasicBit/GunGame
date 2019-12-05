using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemHolder : MonoBehaviour
{
    public GameObject player;
    public GameObject playerCamera;
    public GameObject otherPlayer;
    public GameObject bullet;
    public GameObject bulletContainer;

    public void Start()
    {
        GameSystem.Main(this);
    }

    public void Update()
    {
        GameSystem.OnTick();
    }

    public void LateUpdate()
    {
        GameSystem.OnLateTick();
    }

    public GameObject CreateObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return Instantiate(prefab, position, rotation);
    }

    public GameObject CreateObject(GameObject parent, GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return Instantiate(prefab, position, rotation, parent.transform);
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