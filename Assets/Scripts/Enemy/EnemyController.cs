using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
   public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public Animator animator;

    private Vector3 targetPosition;
    private bool movingToPointA = true;
    private bool isWaiting = false;
    private float minWaitTime = 0.5f;
    private float maxWaitTime = 1f;

    private void Start()
    {
        targetPosition = pointA.position;
    }

    private void Update()
    {
        if (!isWaiting)
        {
            MoveEnemy();
        }
    }

    private void MoveEnemy()
    {
        // Düşmanı hedef noktaya doğru hareket ettir
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Sprite'ın yönünü değiştirme
        FlipSprite();

        // Eğer hedef noktaya ulaşıldıysa, animasyonu güncelle
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            animator.SetBool("IsIdle", true);
            animator.SetBool("IsRun", false);

            // Hedefte bekleme işlemi başlat
            StartCoroutine(WaitAtTarget());
        }
        else
        {
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsRun", true);
        }
    }

    private void FlipSprite()
    {
        if (targetPosition.x < transform.position.x) // Solda hareket
        {
            transform.localScale = new Vector3(-1, 1, 1); // Sprite'ı sola çevir
        }
        else if (targetPosition.x > transform.position.x) // Sağda hareket
        {
            transform.localScale = new Vector3(1, 1, 1); // Sprite'ı normale çevir
        }
    }

    private IEnumerator WaitAtTarget()
    {
        isWaiting = true;

        // Rastgele bekleme süresi belirle
        float waitTime = Random.Range(minWaitTime, maxWaitTime);
        yield return new WaitForSeconds(waitTime);
        
        // Hedef nokta değiştir
        SwitchTarget();
        isWaiting = false;
    }

    private void SwitchTarget()
    {
        if (movingToPointA)
        {
            targetPosition = pointB.position;
        }
        else
        {
            targetPosition = pointA.position;
        }
        movingToPointA = !movingToPointA;
    }
}