using Palmmedia.ReportGenerator.Core.CodeAnalysis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Movement : MonoBehaviour
{
    [Header("Movement Player")]
    public string nameForward;
    public string nameBackward;
    public string nameLeft;
    public string nameRight;
    public float walkSpeed = 1.0f;
    public float currentMove;
    [Header("Jump")]
    public float jumpForce = 5.0f;
    public float gravity = 9.81f;
    public string jumpName;
    public Vector3 velocity;
    [Header("Running")]
    public float RunSpeed = 2.0f;
    public float timeRun = 10f;
    public string runString;
    [Header("Other")]
    public CharacterController characterController;
    [Header("Camera")]
    public Transform CameraTransform;
    public float sensibility = 2.0f;
    public float rotationX = 0;

    private void Start()
    {
        currentMove = walkSpeed;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update()
    {
        float Horizontal = Input.GetKey(nameRight) ? 1.0f : Input.GetKey(nameLeft) ? -1.0f : 0.0f;
        float Vertical = Input.GetKey(nameForward) ? 1.0f : Input.GetKey(nameBackward) ? -1.0f : 0.0f;

        bool isGrounded = characterController.isGrounded;

        if (isGrounded == false)
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        if (isGrounded == true && Input.GetKeyDown(jumpName))
        {
            velocity.y = jumpForce;
        }

        if (Input.GetKey(runString) && timeRun > 0)
        {
            currentMove = RunSpeed;
            timeRun -= 1 * Time.deltaTime;
        }
        else if (!Input.GetKey(runString) || Input.GetKeyUp(runString) || timeRun <= 0) 
        {
            currentMove = walkSpeed;
            timeRun += 0.5f * Time.deltaTime;
        }

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        CameraTransform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0f, mouseX, 0f);

        Vector3 forward = CameraTransform.forward;
        forward.y = 0;
        forward.Normalize();
        Vector3 right = CameraTransform.right;
        right.y = 0;
        right.Normalize();

        Vector3 direction = forward * Vertical + right * Horizontal;
        direction.Normalize();

        characterController.Move((direction * currentMove + velocity) * Time.deltaTime);
    }
}
