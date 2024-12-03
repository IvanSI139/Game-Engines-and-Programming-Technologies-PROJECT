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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        }
    }
}
