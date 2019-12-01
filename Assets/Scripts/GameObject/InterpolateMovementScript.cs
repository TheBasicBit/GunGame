using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterpolateMovementScript : MonoBehaviour
{
    private float startTime;
    private float timeDiff;

    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 diffPos;

    private Vector3 startRot;
    private Vector3 endRot;
    private Vector3 diffRot;

    private bool moving = false;

    public void Update()
    {
        if (moving)
        {
            float state = (Time.time - startTime) / timeDiff;

            if (state >= 1)
            {
                transform.position = endPos;
                transform.eulerAngles = endRot;
            }
            else
            {
                transform.position = startPos + (diffPos * state);
                transform.eulerAngles = startRot + (diffRot * state);
            }
        }
    }

    public void MoveTo(Vector3 pos, Vector3 rot, float seconds)
    {
        startPos = transform.position;
        endPos = pos;
        diffPos = endPos - startPos;

        startRot = transform.eulerAngles;
        endRot = rot;
        diffRot = endRot - startRot;

        startTime = Time.time;
        timeDiff = startTime + seconds - startTime;

        moving = true;
    }
}