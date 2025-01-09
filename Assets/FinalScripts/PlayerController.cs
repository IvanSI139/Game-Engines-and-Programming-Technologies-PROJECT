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
    private bool onWall = false;
    private float vertical;
    [SerializeField] private float climbSpeed;

    private bool isLadder;
    private bool isClimbing;

    // Dash Variables
    public float dashSpeed = 15f; // Speed of the dash
    public float dashDuration = 0.2f; // Duration of the dash
    private bool isDashing = false; // Is the player currently dashing?
    private float dashTime = 0f; // Timer for the dash duration

    [Header("Attack")]
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float damage;
    [SerializeField] private CapsuleCollider2D CapsuleCollider;
    [SerializeField] private LayerMask EnemyLayer;
    private EnemyHealth EnemyHealth;
    private BossHealth BossHealth;

    [Header("SoundFX")]
    [SerializeField] private AudioClip runSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip landingSound;
    [SerializeField] private AudioClip dashSound;
    [SerializeField] private AudioClip swordSwingSound;
    [SerializeField] private AudioClip playerHurt;

    private Animator anim;//animator
    private Health currentHealth;
    private Inventory inventory;
    private AudioManager audioManager;

    private int count;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
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
        //if (moveInput != 0 && isGrounded)
        //{
        //    audioManager.PlaySFX(runSound);
        //}


        // Flip sprite based on movement direction (optional)
        if (moveInput > 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        anim.SetBool("moving", moveInput != 0);
        anim.SetBool("Attack", false);
        anim.SetBool("wall", onWall);

        vertical = Input.GetAxis("Vertical");

        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }

        if (isClimbing)
        {
            if (Mathf.Abs(vertical) > 0f)
            {
                anim.SetBool("Ladder", true);
                anim.SetBool("grounded", true);
                anim.speed = 1f;
            }
            else
            {
                anim.speed = 0f;
            }

        }
        else
        {
            anim.SetBool("Ladder", false);
            anim.SetBool("grounded", isGrounded);
            anim.speed = 1f;
        }

 

        // Jumping
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
            anim.SetBool("grounded", false);
            anim.SetBool("moving", false);

            audioManager.PlaySFX(jumpSound);

        }

        // Start Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            StartDash();
            audioManager.PlaySFX(dashSound);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.K) && isGrounded)
        {
            StartDash();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }



    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * climbSpeed);
        }
        else
        {
            rb.gravityScale = 2f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        // Reset jump count when touching the ground
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
            audioManager.PlaySFX(landingSound);

        }


        int wallLayer = LayerMask.NameToLayer("Wall");
        if (collision.gameObject.layer == wallLayer)
        {
            Debug.Log("Player collided with a wall!");

            foreach (ContactPoint2D contact in collision.contacts)
            {
                Vector2 normal = contact.normal;

                if (Vector2.Dot(normal, Vector2.up) > 0.5f)
                {
                    Debug.Log("Player collided with the top of the wall!");
                }
                else if (Vector2.Dot(normal, Vector2.down) > 0.5f)
                {
                    Debug.Log("Player collided with the bottom of the wall!");
                }
                else if (Vector2.Dot(normal, Vector2.left) > 0.5f)
                {
                    Debug.Log("Player collided with the left side of the wall!");
                    onWall = true;
                }
                else if (Vector2.Dot(normal, Vector2.right) > 0.5f)
                {
                    Debug.Log("Player collided with the right side of the wall!");
                    onWall = true;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }


        int wallLayer = LayerMask.NameToLayer("Wall");
        if (collision.gameObject.layer == wallLayer)
        {
            Debug.Log("Player left a wall!");
            onWall = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int LadderlLayer = LayerMask.NameToLayer("Ladder");
        if (collision.gameObject.layer == LadderlLayer)
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        int LadderlLayer = LayerMask.NameToLayer("Ladder");
        if (collision.gameObject.layer == LadderlLayer)
        {
            isLadder = false;
            isClimbing = false;
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
        audioManager.PlaySFX(swordSwingSound);
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
            BossHealth = hit.transform.GetComponent<BossHealth>();
        }


        return hit.collider != null;
    }
    private void DamageEnemy()
    {
        if (EnemyInRange())
        {
            EnemyHealth.TakeDamage(damage);
            BossHealth .TakeDamage(damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(CapsuleCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(CapsuleCollider.bounds.size.x * range, CapsuleCollider.bounds.size.y, CapsuleCollider.bounds.size.z));
    }

    public void Save(ref PlayerSaveData data)
    {
        data.Position = transform.position;
    }

    public void Load(PlayerSaveData data)
    {
        transform.position = data.Position; 
    }

}


[System.Serializable]

public struct PlayerSaveData
{
    public Vector3 Position;
}