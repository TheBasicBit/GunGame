using BaseSystem.Network.Packets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;

    public float gravity = 15;

    public float walkSpeed = 3;
    public float sprintSpeed = 7;

    public float jumpForce = 6;
    public float jumpWallUpHeight = 100;
    public float stepUpHeight = 50;

    public KeyCode shootKey = KeyCode.Mouse0;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftControl;

    public bool _________________________________________________;

    public float verticalForce;
    public float lastShoot = 0;
    public bool isJumping = false;
    public bool isDoubleJumping = false;
    public bool onGround = false;
    public bool lastOnGround = false;

    public Player()
    {
        verticalForce = -gravity;
    }

    public CharacterController CharacterController { get; set; }
    public bool IsGrounded { get; set; }

    public void Start()
    {
        CharacterController = GetComponent<CharacterController>();
        CharacterController.slopeLimit = stepUpHeight;
    }

    public void Update()
    {
        UpdateMovement();
    }

    public void OnCollisionEnter(Collision collision)
    {
        GameObject gameObject = collision.gameObject;

        if (gameObject.HasComponent<Bullet>())
        {
            gameObject.Destroy();
            Debug.Log("Aua :(");
        }
    }

    public void LostGround()
    {
        if (!isJumping)
        {
            verticalForce = 0;
        }
    }

    public void UpdateMovement()
    {
        lastOnGround = onGround;
        onGround = CharacterController.isGrounded;

        if (onGround != lastOnGround && !onGround)
        {
            LostGround();
        }

        if (Input.GetKeyDown(jumpKey))
        {
            if (onGround && !isJumping)
            {
                Jump();
            }
            else if (isJumping && !isDoubleJumping)
            {
                Jump();
            }
        }

        if (Input.GetKey(shootKey))
        {
            if (Time.time - lastShoot > 0.1)
            {
                Shoot();
            }
        }

        if (verticalForce > -gravity)
        {
            verticalForce -= Time.deltaTime * gravity;
        }

        if (verticalForce < -gravity)
        {
            verticalForce = -gravity;
        }

        float speed = Input.GetKey(sprintKey) ? sprintSpeed : walkSpeed;

        Vector3 vectorXZ = new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime);
        Vector3 vectorY = new Vector3(0, verticalForce * Time.deltaTime, 0);

        CharacterController.Move(transform.TransformDirection(Vector3.ClampMagnitude(vectorXZ, 1) + vectorY));

        if (onGround && verticalForce < 0)
        {
            isJumping = false;
            isDoubleJumping = false;
            CharacterController.slopeLimit = stepUpHeight;
        }
    }

    public void Jump()
    {
        if (isJumping)
        {
            isDoubleJumping = true;
        }

        isJumping = true;

        CharacterController.slopeLimit = jumpWallUpHeight;

        verticalForce = jumpForce;
    }

    public void Shoot()
    {
        lastShoot = Time.time;
        Vector3 startPosition = transform.position + new Vector3(0, 0.25f, 0) + GameSystem.PlayerCamera.transform.TransformDirection(new Vector3(0, 0, 0.25f));
        Vector3 rotation = GameSystem.PlayerCamera.transform.eulerAngles;

        NetworkManager.SendPacket(new ShootPacket()
        {
            posX = startPosition.x,
            posY = startPosition.y,
            posZ = startPosition.z,
            rotX = rotation.x,
            rotY = rotation.y,
            rotZ = rotation.z
        });
    }
}