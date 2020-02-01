using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlindDeer.Game.PiouPiou
{
    public class PlayerCamera : MonoBehaviour
    {
        public float cameraRotationSpeed = 3;

        public float cameraWalkAnimationPower = 3;

        public KeyCode switchMouseLocked = KeyCode.Escape;

        public bool __________________________________________;

        public float yaw;
        public float pitch;

        public float startPosY;
        public float currentCameraWalkAnimationPower;
        public float currentCameraWalkAnimationPowerVertical;

        public bool mouseLocked = true;

        public Player Player
        {
            get
            {
                return gameObject.GetComponentInParent<Player>();
            }
        }

        public void Start()
        {
            startPosY = transform.localPosition.y;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void Update()
        {
            if (Input.GetKeyDown(switchMouseLocked))
            {
                mouseLocked = !mouseLocked;
            }

            if (mouseLocked)
            {
                SetRotation(yaw + (cameraRotationSpeed * Input.GetAxis("Mouse X")), pitch - (cameraRotationSpeed * Input.GetAxis("Mouse Y")));
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }

            float endCameraWalkAnimationPower = 0;

            if (Player.isWalking)
            {
                endCameraWalkAnimationPower = (float)Math.Sin(Time.time * 10) / 20 * cameraWalkAnimationPower;
            }

            if (currentCameraWalkAnimationPower != endCameraWalkAnimationPower)
            {
                if (currentCameraWalkAnimationPower > endCameraWalkAnimationPower)
                {
                    currentCameraWalkAnimationPower -= Time.deltaTime * 10;

                    if (currentCameraWalkAnimationPower < endCameraWalkAnimationPower)
                    {
                        currentCameraWalkAnimationPower = endCameraWalkAnimationPower;
                    }
                }
                else
                {
                    currentCameraWalkAnimationPower += Time.deltaTime * 10;

                    if (currentCameraWalkAnimationPower > endCameraWalkAnimationPower)
                    {
                        currentCameraWalkAnimationPower = endCameraWalkAnimationPower;
                    }
                }
            }

            transform.localPosition = new Vector3(currentCameraWalkAnimationPower, startPosY - ((currentCameraWalkAnimationPower < 0 ? currentCameraWalkAnimationPower * -1 : currentCameraWalkAnimationPower) / 1.75f), transform.localPosition.z);
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

            Player.transform.eulerAngles = new Vector3(0, yaw, 0);
            transform.eulerAngles = new Vector3(pitch, yaw, 0);
        }
    }
}