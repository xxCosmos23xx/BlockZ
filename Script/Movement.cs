using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement Player")]
    public string nameForward;
    public string nameBackward;
    public string nameLeft;
    public string nameRight;
    public float walkSpeed = 1.0f;
    public float currentMove;
    [Header("Gravity")]
    public float gravity = 9.81f;
    public Vector3 velocity;
    [Header("Other")]
    public CharacterController characterController;

    private void Start()
    {
        currentMove = walkSpeed;
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

        Vector3 direction = new Vector3 (Horizontal, 0.0f, Vertical).normalized;

        characterController.Move((direction * currentMove + velocity) * Time.deltaTime);
    }
}
