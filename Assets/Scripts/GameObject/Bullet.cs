using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float power;
    public float maxFlyWide;

    public bool _________________________________________________;

    public Vector3 startPosition;

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
        if (collision.gameObject.layer == (int)Layers.Terrain)
        {
            Destroy(gameObject);
        }
    }
}