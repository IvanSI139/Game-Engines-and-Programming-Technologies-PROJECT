using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    private float vertical;
    [SerializeField] private float speed;
    private bool isLadder;
    private bool isClimbing;
    [SerializeField] private Rigidbody2D rb;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        vertical = Input.GetAxis("Vertical");

        if (isLadder && Mathf.Abs(vertical) > 0f ) 
        { 
            isClimbing = true;
        }

        if (isClimbing)
        {
            if (Mathf.Abs(vertical) > 0f )
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
            anim.speed = 1f;
        }

        
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
        }
        else
        {
            rb.gravityScale = 2f;
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
}
