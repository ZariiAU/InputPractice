using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    CharacterController characterController;
    Rigidbody rb;
    Vector2 input;
    [Header("Movement Settings")]
    public float speed = 5;
    public float sprintSpeed = 10;
    public float checkGroundedRange = 0.2f;
    public float jumpStrength;
    [Header("Camera Settings")]
    public Camera cam;
    public float yDownLimit = 70;
    public float yUpLimit = -80;
    public float sensitivity = 1;
    public bool invertCameraY = false;
    public bool invertCameraX = false;
    float xRotationAmount = 0;
    float yRotationAmount = 0;
    Vector3 velocity;
     RaycastHit hit;
    float rayLength = 1;
    bool isGrounded = false;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Look();
        CheckGrounded();
    }
    void CheckGrounded()
    {
        rayLength = (characterController.height / 2) + checkGroundedRange;

        if (Physics.Raycast(transform.position, -transform.up, out hit, rayLength))
        {
            velocity.y = 0;

            isGrounded = true;
        }
        else
            isGrounded=false;
        Debug.DrawRay(transform.position, -transform.up * rayLength, Color.red);
    }
    void Look()
    {
        // Rotate the character on the Y axis
        if (Input.GetAxis("Mouse X") != 0)
        {
            if (invertCameraX)
                yRotationAmount += -Input.GetAxis("Mouse X") * sensitivity;
            else
                yRotationAmount += Input.GetAxis("Mouse X") * sensitivity;

        }
        // Rotate on X axis
        if (Input.GetAxis("Mouse Y") != 0)
        {
            if (invertCameraY)
                xRotationAmount += Input.GetAxis("Mouse Y") * sensitivity;
            else
                xRotationAmount += -Input.GetAxis("Mouse Y") * sensitivity;

            xRotationAmount = Mathf.Clamp(xRotationAmount, -80, 70);

        }
        // Rotate the whole character when moving the mouse horizontally
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotationAmount);
        // Rotate just the camera when moving the mouse vertically
        cam.transform.eulerAngles = new Vector3(xRotationAmount, transform.eulerAngles.y);
    }

    void Move()
    {
        // Get Inputs
        if (Input.GetAxis("Horizontal") != 0)
            input.x = Input.GetAxis("Horizontal");
        if (Input.GetAxis("Vertical") != 0)
            input.y = Input.GetAxis("Vertical");
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            input = Vector2.zero;

        // Apply Gravity
        velocity.y += Physics.gravity.y * Time.deltaTime;

        // Set our velocity to be based on our character's current orientation
        var temp = velocity.y;
        velocity = (transform.forward * input.y + transform.right * input.x);
        velocity.y = temp;

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpStrength * -Physics.gravity.y);
        }

        // Check sprint but exclude Y speed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocity.x *= sprintSpeed;
            velocity.z *= sprintSpeed;
        }
        else
        {
            velocity.x *= speed;
            velocity.z *= speed;
        }

        characterController.Move(velocity * Time.deltaTime);

    }
}