using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    public float jumpForce = 10f; // Jump force
    private Rigidbody2D rb; // Reference to the Rigidbody2D
    private int jumpCount = 0; // Track number of jumps
    public int maxJumps = 2; // Maximum allowed jumps
    private bool isGrounded = false; // Check if the player is on the ground

    // Dash Variables
    public float dashSpeed = 15f; // Speed of the dash
    public float dashDuration = 0.2f; // Duration of the dash
    private bool isDashing = false; // Is the player currently dashing?
    private float dashTime = 0f; // Timer for the dash duration

    [Header("Attack")]
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    [SerializeField] private CapsuleCollider2D CapsuleCollider;
    [SerializeField] private LayerMask EnemyLayer;
    private EnemyHealth EnemyHealth;



    private Animator anim;//animator
    private Health currentHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDashing)
        {
            PerformDash();
            return; // Skip other movement logic while dashing
        }

        // Movement
        float moveInput = Input.GetAxis("Horizontal"); // Get left/right input
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Flip sprite based on movement direction (optional)
        if (moveInput > 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        anim.SetBool("moving", moveInput != 0);
        anim.SetBool("grounded", isGrounded);
        anim.SetBool("Attack", false);



        // Jumping
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
        }

        // Start Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            StartDash();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.K) && isGrounded)
        {
            StartDash();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Reset jump count when touching the ground
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    // Start Dash Logic
    void StartDash()
    {
        isDashing = true;
        dashTime = dashDuration;
        anim.SetBool("dashing", isDashing);
    }

    // Perform Dash Logic
    void PerformDash()
    {
        dashTime -= Time.deltaTime;

        // Determine dash direction based on facing
        float dashDirection = transform.localScale.x > 0 ? 1 : -1;

        // Set dash velocity
        rb.velocity = new Vector2(dashDirection * dashSpeed, rb.velocity.y);

        // End dash when time expires
        if (dashTime <= 0)
        {
            isDashing = false;
            anim.SetBool("dashing", isDashing);
        }
    }

    void Attack()
    {
        anim.SetBool("Attack", true);
    }


    public bool EnemyInRange()
    {
        RaycastHit2D hit =
                Physics2D.CapsuleCast(CapsuleCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
                new Vector3(CapsuleCollider.bounds.size.x * range, CapsuleCollider.bounds.size.y, CapsuleCollider.bounds.size.z),
                0, 0, Vector2.left, EnemyLayer);

        if (hit.collider != null)
        {
            EnemyHealth = hit.transform.GetComponent<EnemyHealth>();
        }


        return hit.collider != null;
    }
    private void DamageEnemy()
    {
        if (EnemyInRange())
        {
            EnemyHealth.TakeDamage(damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(CapsuleCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(CapsuleCollider.bounds.size.x * range, CapsuleCollider.bounds.size.y, CapsuleCollider.bounds.size.z));
    }

}
