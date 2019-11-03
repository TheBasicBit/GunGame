using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float power;
    public float maxFlyWide;

    public bool _________________________________________________;

    public Vector3 startPosition;

    public void Update()
    {
        transform.position += transform.TransformDirection(new Vector3(0, 0, Time.deltaTime * power));

        if (Vector3.Distance(startPosition, transform.position) > maxFlyWide)
        {
            Destroy(gameObject);
        }
    }
}