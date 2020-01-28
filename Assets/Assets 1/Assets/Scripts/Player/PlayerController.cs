using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject inventory;
    public Transform groundCheck;
    public LayerMask groundLayers;

    public float speed;
    public float jumpHeight;
    public float gravity;
    public float groundCheckRadius;
    public float force;
    public float radius;

    public bool canMove;
    public bool canNotBeAimed = true;
    public bool crouch;

    private CharacterController controller;
    private Vector3 velocity;

    private bool isGrounded;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inventory.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (inventory.activeInHierarchy == false)
        {
            GetComponentInChildren<CameraController>().Camera();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventory.activeInHierarchy == true)
            {
                inventory.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else if (inventory.activeInHierarchy == false)
            {
                inventory.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
        if (inventory.activeInHierarchy == false)
        {
            CalculateMovement();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouch = true;
            controller.height = 1;
            controller.center = new Vector3(0, -.5f, 0);
            Camera.main.transform.position -= new Vector3(0, 0.5f, 0);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            crouch = false;
            controller.height = 2;
            controller.center = new Vector3(0, 0, 0);
            Camera.main.transform.position += new Vector3(0, 0.5f, 0);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayers);
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2;
            }

            if (Input.GetButtonDown("Jump") && isGrounded && canMove && !canNotBeAimed)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
    }

    private void CalculateMovement()
    {
        float mHorizontal = Input.GetAxisRaw("Horizontal");
        float mVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = transform.right * mHorizontal + transform.forward * mVertical;

        if (canMove && !canNotBeAimed)
        {
            controller.Move(movement * speed * Time.deltaTime);
        }else
        {
            controller.Move(movement * (speed / 3) * Time.deltaTime);
        }
    }
}
