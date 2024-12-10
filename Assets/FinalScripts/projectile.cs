using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hit;
    private float direction;
    private float lifetime;

    private Animator anim;

    private CapsuleCollider2D CapColloder;



    private void Awake()
    {
        anim = GetComponent<Animator>();
        CapColloder = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        if (hit) return;
        float moveSpeed = speed * Time.deltaTime * direction;  
        transform.Translate(moveSpeed, 0, 0);

        lifetime += Time.deltaTime;

        if (lifetime > 5) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        CapColloder.enabled = false;
        anim.SetTrigger("expload");
    }

    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction; 
        gameObject.SetActive(true);
        hit =false;
        CapColloder.enabled=true;

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

       transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);   
    }

    public void Save(ref ProjectileData data)
    {
        data.Position = transform.position;
        data.lifetime = lifetime;
    }

    public void Load(ProjectileData data)
    {
        transform.position = data.Position;
        lifetime = data.lifetime;


    }
}

[System.Serializable]

public struct ProjectileData
{
    public Vector3 Position;
    public float lifetime;
}
