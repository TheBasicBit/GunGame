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

    private int moveMode = -1;

    public void Update()
    {
        if (moveMode > -1)
        {
            float state = (Time.time - startTime) / timeDiff;

            if (moveMode == 0)
            {
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
            else if (moveMode == 1)
            {
                transform.localEulerAngles = startRot + (diffRot * state);
            }
        }
    }

    public void MoveTo(Vector3 pos, Vector3 rot, float seconds)
    {
        startPos = transform.position;
        endPos = pos;
        diffPos = endPos - startPos;

        startRot = transform.localEulerAngles;
        endRot = rot.ToAngle();

        Vector3 diff = (endRot - startRot).ToAngle();

        if (diff.x > 180)
        {
            diff = new Vector3(diff.x - 360, diff.y, diff.z);
        }
        else if (diff.x < -180)
        {
            diff = new Vector3(diff.x + 360, diff.y, diff.z);
        }

        if (diff.y > 180)
        {
            diff = new Vector3(diff.x, diff.y - 360, diff.z);
        }
        else if (diff.y < -180)
        {
            diff = new Vector3(diff.x, diff.y + 360, diff.z);
        }

        if (diff.y > 180)
        {
            diff = new Vector3(diff.x, diff.y, diff.z - 360);
        }
        else if (diff.y < -180)
        {
            diff = new Vector3(diff.x, diff.y, diff.z + 360);
        }

        diffRot = diff;

        startTime = Time.time;
        timeDiff = startTime + seconds - startTime;

        moveMode = 0;
    }

    public void MoveLocalEulerAnglesTo(Vector3 rot, float seconds)
    {
        startRot = transform.localEulerAngles;
        endRot = rot.ToAngle();

        Vector3 diff = (endRot - startRot).ToAngle();

        if (diff.x > 180)
        {
            diff = new Vector3(diff.x - 360, diff.y, diff.z);
        }
        else if (diff.x < -180)
        {
            diff = new Vector3(diff.x + 360, diff.y, diff.z);
        }

        if (diff.y > 180)
        {
            diff = new Vector3(diff.x, diff.y - 360, diff.z);
        }
        else if (diff.y < -180)
        {
            diff = new Vector3(diff.x, diff.y + 360, diff.z);
        }

        if (diff.y > 180)
        {
            diff = new Vector3(diff.x, diff.y, diff.z - 360);
        }
        else if (diff.y < -180)
        {
            diff = new Vector3(diff.x, diff.y, diff.z + 360);
        }

        diffRot = diff;

        startTime = Time.time;
        timeDiff = startTime + seconds - startTime;

        moveMode = 1;
    }
}