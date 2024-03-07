using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [Header("Move")]
    [SerializeField] private float moveSpeed = 8;
    [SerializeField] private float maxSpeed = 10, minusSpeed = 5, constantSpeed = 8; //cameraOffset;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 3;
    [SerializeField] private float counterJumpForce = -3, maxHold = 5;
    private float holdTimer = 0;

    private MoonPatrolInput input = null;
    private Rigidbody rb = null;
    //private Camera mainCamera;
    //private Vector3 mainCameraPos;
    private Vector2 moveVector = Vector2.zero;
    //private float cameraStartXPos;
    private bool isJumping, isGrounded;

    private void Awake()
    {
        instance = this;

        input = new MoonPatrolInput();
        rb = GetComponent<Rigidbody>();
    }

    //private void Start()
    //{
    //    mainCamera = Camera.main;

    //    cameraStartXPos = mainCameraPos.x;
    //}

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
        //mainCameraPos = mainCamera.transform.position;
        //Debug.Log(mainCameraPos);
        transform.position += transform.right * moveSpeed * Time.deltaTime;

        if (moveVector.x > 0)
        {
            moveSpeed = maxSpeed;
            //mainCameraPos = new Vector3(cameraStartXPos + cameraOffset, mainCameraPos.y, mainCameraPos.z);
        }
        else if (moveVector.x < 0)
        {
            moveSpeed = minusSpeed;
            //mainCameraPos = new Vector3(cameraStartXPos - cameraOffset, mainCameraPos.y, mainCameraPos.z);
        }
        else
        {
            moveSpeed = constantSpeed;
            //mainCameraPos = new Vector3(cameraStartXPos, mainCameraPos.y, mainCameraPos.z);
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

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Shield"))
        {
            PlayerManager.instance.ShieldPickUp();
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
}
