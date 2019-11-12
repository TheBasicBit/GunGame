using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterpolateMovementScript : MonoBehaviour
{
    private float timer;
    private float progress;
    private Vector3 difference;
    private Vector3 start;
    private float time;
    private Vector3 point;

    public void Start()
    {
        time = 0;
        start = transform.position;
        point = transform.position;
        difference = point - start;
    }

    public void Update()
    {
        if (timer <= time)
        {
            timer += Time.deltaTime;

            progress = timer / time;
            transform.position = start + difference * progress;
        }
    }

    public void MoveTo(Vector3 pos, float seconds)
    {
        time = seconds;
        point = pos;
        start = transform.position;
        difference = point - start;
    }
}