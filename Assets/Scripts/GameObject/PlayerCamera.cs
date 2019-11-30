using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float cameraRotationSpeed = 3;

    public float cameraWalkAnimationPower = 3;
    public float currentCameraWalkAnimationPower;

    public float interpolationSpeed = 1;

    public bool _________________________________________________;

    public float yaw;
    public float pitch;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update()
    {
        SetRotation(yaw + (cameraRotationSpeed * Input.GetAxis("Mouse X")), pitch - (cameraRotationSpeed * Input.GetAxis("Mouse Y")));

        float endCameraWalkAnimationPower = (float)Math.Sin(Time.time);

        transform.localPosition = new Vector3(endCameraWalkAnimationPower, transform.localPosition.y, transform.localPosition.z);
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

        GameSystem.Player.transform.eulerAngles = new Vector3(0, yaw, 0);
        transform.eulerAngles = new Vector3(pitch, yaw, 0);
    }
}