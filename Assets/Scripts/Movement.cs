using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    CharacterController characterController;
    Vector2 input;
    public float speed = 1;
    public Camera cam;
    public float yDownLimit = 70;
    public float yUpLimit = -80;
    public float sensitivity = 1;
    public bool invertCameraY = false;
    public bool invertCameraX = false;
    float xRotationAmount = 0;
    float yRotationAmount = 0;


    // Start is called before the first frame update
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Look();
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
        if (Input.GetAxis("Horizontal") != 0)
        {
            input.x = Input.GetAxis("Horizontal");
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            input.y = Input.GetAxis("Vertical");
        }
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            input = Vector2.zero;
        }

        // Set our velocity to be based on our character's current orientation
        Vector3 velocity = (transform.forward * input.y + transform.right * input.x) * speed;
        characterController.Move(velocity * Time.deltaTime);

    }
}
