using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8, maxSpeed = 10, minusSpeed = 5, constantSpeed = 8;

    private MoonPatrolInput input = null;
    private Rigidbody rb = null;
    private Vector2 moveVector = Vector2.zero;

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
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Move.performed -= OnMovementPerformed;
        input.Player.Move.canceled -= OnMovementCancelled;
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
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
    }
}
