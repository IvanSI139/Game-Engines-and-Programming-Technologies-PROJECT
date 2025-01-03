using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class rangedAI : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireball;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Parameters")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;
    private Health currentHealth;

    private EnemyPatrol enemyPatrol;
    private FolowPlayer folowPlayer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
        folowPlayer = GetComponentInParent<FolowPlayer>();
        anim.SetBool("Attack", false);
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {

            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetBool("Attack", true);
            }

            else
            {
                anim.SetBool("Attack", false);
            }


        }

        else
        {
            anim.SetBool("Attack", false);
        }

        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }

        if (folowPlayer != null)
        {
            folowPlayer.enabled = !PlayerInSight();
        }
    }


    private void RangedAttack()
    {
        cooldownTimer = 0;
        fireball[FindFireball()].transform.position = firePoint.position;
        fireball[FindFireball()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireball.Length; i++)
        {
            if (!fireball[i].activeInHierarchy)
                return i;
        }
        return 0;

    }
    public bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }


    public void Save(ref RangedEData data)
    {
        data.Position = transform.position;
        data.cooldownTimer = cooldownTimer;
    }

    public void Load(RangedEData data)
    {
        transform.position = data.Position;
        cooldownTimer = data.cooldownTimer;
    }

}

[System.Serializable]

public struct RangedEData
{
    public Vector3 Position;
    public float cooldownTimer;
}

