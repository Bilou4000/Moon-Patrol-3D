using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [Header("Move")]
    [SerializeField] private float moveSpeed = 8;
    [SerializeField] private float maxSpeed = 10, minusSpeed = 5, constantSpeed = 8;
    private Vector2 moveVector = Vector2.zero;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 3;
    [SerializeField] private float counterJumpForce = -3, maxHold = 5;
    private float holdTimer = 0;
    private bool isJumping, isGrounded;

    [Header("Camera")]
    [SerializeField] private float cameraOffset = 2;
    [SerializeField] private float cameraTravelSpeed;
    private Camera mainCamera;
    private Vector3 mainCameraPos;
    private float cameraStartXPos;

    [Header("Shield")]
    [SerializeField] float flashInterval;
    [SerializeField] private GameObject Ekey, TriangleKey, shieldImage;

    [Header("Others")]
    [SerializeField]GameObject dustTrail;
    Vector4 dustColor;
   

    private MoonPatrolInput input = null;
    private Rigidbody rb = null;
    private bool hasShield;

    private void Awake()
    {
        instance = this;

        input = new MoonPatrolInput();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
        mainCameraPos = mainCamera.transform.localPosition;


        cameraStartXPos = mainCameraPos.x;

        dustColor = dustTrail.GetComponent<ParticleSystem>().startColor;

        Ekey.SetActive(false);
        TriangleKey.SetActive(false);
        shieldImage.SetActive(false);
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

    private void Update()
    {
        if (hasShield)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Ekey.SetActive(false);
                TriangleKey.SetActive(false);
                shieldImage.SetActive(false);
                hasShield = false;

                PlayerManager.instance.ShieldPickUp();

            }
        }
    }


    private void FixedUpdate()
    {
        transform.position -= transform.right * moveSpeed * Time.deltaTime;
        mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, mainCameraPos, cameraTravelSpeed * Time.deltaTime);

        if (moveVector.x > 0)
        {
            moveSpeed = maxSpeed;

            mainCameraPos = new Vector3(cameraStartXPos + cameraOffset, mainCameraPos.y, mainCameraPos.z);


        }
        else if (moveVector.x < 0)
        {
            moveSpeed = minusSpeed;

            mainCameraPos = new Vector3(cameraStartXPos - cameraOffset, mainCameraPos.y, mainCameraPos.z);
        }
        else
        {
            moveSpeed = constantSpeed;

            mainCameraPos = new Vector3(cameraStartXPos, mainCameraPos.y, mainCameraPos.z);
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

        if (isGrounded)
        {
            dustTrail.GetComponent<ParticleSystem>().startColor = dustColor;
        }
        else
        {
            dustTrail.GetComponent<ParticleSystem>().startColor = new Vector4(221, 168, 134, 0.0f);
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
            if (!hasShield)
            {
                hasShield = true;
                shieldImage.SetActive(true);

                StartCoroutine(FlashingInput(false));

            }
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


    private IEnumerator FlashingInput(bool isUsingGamepad)
    {
        while(hasShield)
        {
            if (isUsingGamepad)
            {
                Ekey.SetActive(false);
                TriangleKey.SetActive(true);
                TriangleKey.transform.GetChild(0).gameObject.SetActive(true);

                yield return new WaitForSeconds(flashInterval);

                TriangleKey.transform.GetChild(0).gameObject.SetActive(false);

                yield return new WaitForSeconds(flashInterval);
            }

            else if (!isUsingGamepad)
            {
                TriangleKey.SetActive(false);
                Ekey.SetActive(true);
                Ekey.transform.GetChild(0).gameObject.SetActive(true);

                yield return new WaitForSeconds(flashInterval);

                Ekey.transform.GetChild(0).gameObject.SetActive(false);

                yield return new WaitForSeconds(flashInterval);
            }
        }
    }
}
