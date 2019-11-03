using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float speed = 3;

    public bool _________________________________________________;

    public float yaw;
    public float pitch;

    public void Start()
    {
        Player.PlayerInstance.Camera = this;

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update()
    {
        SetRotation(yaw + (speed * Input.GetAxis("Mouse X")), pitch - (speed * Input.GetAxis("Mouse Y")));
    }

    public void SetRotation(float yaw, float pitch)
    {
        if (pitch > 90)
        {
            pitch = 90;
        }
        else if (pitch < -90)
        {
            pitch = -90;
        }

        this.yaw = yaw;
        this.pitch = pitch;

        Player.PlayerInstance.transform.eulerAngles = new Vector3(0, yaw, 0);
        transform.eulerAngles = new Vector3(pitch, yaw, 0);
    }
}