using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterpolateMovementScript : MonoBehaviour
{
    private float startTime;
    private float endTime;
    private float timeDiff;

    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 diff;

    private bool moving = false;

    public void Update()
    {
        if (moving)
        {
            float state = (Time.time - startTime) / timeDiff;

            if (state >= 1)
            {
                transform.position = endPos;
            }
            else
            {
                transform.position = startPos + (diff * state);
            }
        }
    }

    public void MoveTo(Vector3 pos, float seconds)
    {
        startPos = transform.position;
        endPos = pos;
        diff = endPos - startPos;

        startTime = Time.time;
        endTime = startTime + seconds;
        timeDiff = endTime - startTime;

        moving = true;
    }
}