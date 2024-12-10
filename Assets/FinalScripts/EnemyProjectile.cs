using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    [SerializeField] protected float damage;
    private float lifetime;
    private Animator anim;
    private CapsuleCollider2D coll;

    private bool hit;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<CapsuleCollider2D>();
    }

    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;
    }
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        //base.OnTriggerEnter2D(collision);
        coll.enabled = false;

        if (anim != null)
            anim.SetTrigger("exploding");
        else
            gameObject.SetActive(false);

        if (collision.tag == "Player")
            collision.GetComponent<Health>().TakeDamage(damage);
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
    public void Save(ref EProjectileData data)
    {
        data.Position = transform.position;
        data.lifetime = lifetime;
    }

    public void Load(EProjectileData data)
    {
        transform.position = data.Position;
        lifetime = data.lifetime;


    }
}

[System.Serializable]

public struct EProjectileData
{
    public Vector3 Position;
    public float lifetime;
}