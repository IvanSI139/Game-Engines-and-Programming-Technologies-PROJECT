using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolowPlayer : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform Player;
    [SerializeField] private Transform enemy;
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool moivingLeft;
    [SerializeField] private Animator anim;

    // Update is called once per frame

    private void Awake()
    {
        initScale = enemy.localScale;

    }
    void Update()
    {
        if (PlayerInSight())
        {
            Debug.Log("in sight");
            if(enemy.position.x >= Player.position.x)
            {
                FollowingPlayer(-1);
            }
            else
            {
                FollowingPlayer(1);
            }
        }
        else
        {
            anim.SetBool("moving", false);
        }
    }
    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }
    private void FollowingPlayer(int _direction)
    {
        anim.SetBool("moving", true);

        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction * 1,
            initScale.y, initScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.x);

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
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
