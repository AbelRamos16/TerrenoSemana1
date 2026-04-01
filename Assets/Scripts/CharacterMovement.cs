using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    public Animator animator;
    public ThirdPersonCamera thirdPersonCamera;

    public float walkSpeed = 4f;
    public float runSpeed = 7f;
    public float rotationSmoothSpeed = 10f;
    public float acceleration = 10f;
    public float jumpForce = 5f;

    public Transform groundCheck;
    public float groundDistance = 0.15f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private Vector3 currentMoveVelocity;
    private Vector3 smoothedMoveDirection;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Update()
    {
        CheckGround();
        HandleAnimation();
        HandleJump();
        HandleAttack();  // Aquí agregamos el manejo de ataque
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
        animator.SetBool("isGrounded", isGrounded);
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (inputDirection.magnitude > 0.1f)
        {
            float cameraYaw = thirdPersonCamera.GetYaw();
            Quaternion cameraRotation = Quaternion.Euler(0f, cameraYaw, 0f);

            Vector3 targetMoveDirection = cameraRotation * inputDirection;

            smoothedMoveDirection = Vector3.SmoothDamp(
                smoothedMoveDirection,
                targetMoveDirection,
                ref currentMoveVelocity,
                1f / acceleration
            );

            bool sprinting = Input.GetKey(KeyCode.LeftShift);
            float speed = sprinting ? runSpeed : walkSpeed;

            Vector3 newPosition = rb.position + smoothedMoveDirection * speed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);

            Quaternion targetRotation = Quaternion.LookRotation(smoothedMoveDirection);
            Quaternion smoothRotation = Quaternion.Slerp(
                rb.rotation,
                targetRotation,
                rotationSmoothSpeed * Time.fixedDeltaTime
            );

            rb.MoveRotation(smoothRotation);
        }
        else
        {
            smoothedMoveDirection = Vector3.zero;
        }
    }

    void HandleAnimation()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 input = new Vector2(horizontal, vertical);
        float moveAmount = Mathf.Clamp01(input.magnitude);

        animator.SetFloat("moveSpeed", moveAmount);
        animator.SetBool("isRunning", moveAmount > 0.1f);
        animator.SetBool("isMovingBackwards", vertical < -0.1f);
        animator.SetBool("isGrounded", isGrounded);
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z); // Resetear el movimiento vertical
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("JumpStart");
            isGrounded = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true; // Establecer que el personaje está en el suelo cuando colisiona con el terreno
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false; // Establecer que el personaje ha dejado de estar en el suelo cuando sale del contacto
    }

    void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0)) // Detecta clic izquierdo
        {
            animator.SetTrigger("Attack");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Lanza un raycast desde el ratón

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Zombie")) // Si el raycast golpea un objeto con el tag "Zombie"
                {
                    Destroy(hit.collider.gameObject); // Elimina el zombie
                }
            }
        }
    }
}