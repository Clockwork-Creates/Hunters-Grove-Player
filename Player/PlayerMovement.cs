using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform playCam;
    public Transform movRotObj;
    public Transform jumpCheck;
    public Rigidbody rb;
    public GameObject arms;
    public LayerMask notPlayer;

    public float runSpeed;
    public float walkSpeed;
    public float armRotTime;
    public float jumpCheckRadius;
    public float jumpForce;

    Vector3 movDirection;
    Vector3 armRotVelocity;
    Vector2 lastMovementInput;

    float speed;

    bool running;
    bool canJump;

    void Start()
    {
        playCam = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float angle;

        Vector2 movInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (canJump)
        {
            lastMovementInput = movInput;
        }
        else
        {
            lastMovementInput = Vector2.zero;
        }
        movDirection = lastMovementInput.normalized;

        // movRotObj rotates itself to the camera's rotation on the y-axis only, so the player woves in its direction
        angle = (Mathf.Atan2(movDirection.x, movDirection.y) * Mathf.Rad2Deg) + playCam.eulerAngles.y;
        movRotObj.eulerAngles = Vector3.up * angle;

        // BTP jumpcheck
        canJump = Physics.OverlapSphere(jumpCheck.position, jumpCheckRadius, notPlayer).Length != 0;
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            // Jumping with no air control
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            rb.AddForce(movRotObj.forward * jumpForce * movInput.magnitude, ForceMode.Impulse);
        }
    }

    void LateUpdate()
    {
        arms.transform.eulerAngles = Vector3.up * playCam.eulerAngles.y;
    }

    void FixedUpdate()
    {
        bool running = (Input.GetKey(KeyCode.LeftShift) && Input.GetAxisRaw("Vertical") > 0.5f);
        speed = running ? runSpeed : walkSpeed;

        if (movDirection != Vector3.zero)
        {
            Vector3 movVelocity = movRotObj.forward * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movVelocity);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(jumpCheck.position, jumpCheckRadius);
    }
}
