using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSpeed = 8;
    [SerializeField] private float maxSpeed = 10, minusSpeed = 5, constantSpeed = 8;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 3;
    [SerializeField] private float counterJumpForce = -3, maxHold = 5, holdTimer = 3;

    private MoonPatrolInput input = null;
    private Rigidbody rb = null;
    private Vector2 moveVector = Vector2.zero;
    private bool isJumping, isGrounded;

    private void Awake()
    {
        input = new MoonPatrolInput();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Move.performed += OnMovementPerformed;
        input.Player.Move.canceled += OnMovementCancelled;
        input.Player.Jump.performed += OnJumpPerformed;
        input.Player.Jump.canceled += OnJumpCancelled;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Move.performed -= OnMovementPerformed;
        input.Player.Move.canceled -= OnMovementCancelled;
        input.Player.Jump.performed -= OnJumpPerformed;
        input.Player.Jump.canceled -= OnJumpCancelled;
    }

    private void FixedUpdate()
    {
        transform.position += transform.right * moveSpeed * Time.deltaTime;

        if (moveVector.x > 0)
        {
            moveSpeed = maxSpeed;
        }
        else if (moveVector.x < 0)
        {
            moveSpeed = minusSpeed;
        }
        else
        {
            moveSpeed = constantSpeed;
        }

        if (isJumping && holdTimer < maxHold && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            holdTimer += Time.fixedDeltaTime;
        }

        if(rb.velocity.y < 0)
        {
            rb.AddForce(Vector3.up * counterJumpForce, ForceMode.Impulse);
        }
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
    }

    private void OnJumpPerformed(InputAction.CallbackContext value)
    {
        isJumping = true;
    }

    private void OnJumpCancelled(InputAction.CallbackContext value)
    {
        isJumping = false;
        holdTimer = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
