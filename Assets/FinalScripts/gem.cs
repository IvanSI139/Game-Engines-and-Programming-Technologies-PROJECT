using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gem : MonoBehaviour
{
    [SerializeField]
    private Behaviour[] components;
    public SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    // Start is called before the first frame update

    private void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        rb.gravityScale = 0f;

        Debug.Log("Get start");
    }
    public void Gem ()
    {
        Debug.Log("Get load");
        foreach (Behaviour component in components)
        {
            component.enabled = true;
        }
        this.spriteRenderer.enabled = true;
        rb.gravityScale = 1f;
    }
}
