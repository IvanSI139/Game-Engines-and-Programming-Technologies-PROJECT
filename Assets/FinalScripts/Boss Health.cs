using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;//animator
    private bool dead;
    Renderer m_Renderer;

    [Header("iFrames")]
    //when hurt

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    [SerializeField] private ParticleSystem BleadingParticles;
    private Vector2 attackDirection;
    public string playerTag = "Player";

    private ParticleSystem BleadingParticlesInstance;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        dead = false;
        m_Renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (dead && !m_Renderer.isVisible)
        {
            gameObject.SetActive(false);
        }

        if (dead && currentHealth > 0)
        {
            dead = false;

            gameObject.SetActive(true);

            foreach (Behaviour component in components)
            {
                component.enabled = true;
            }

            anim.SetTrigger("Load");
        }
    }
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);


        if (currentHealth > 0)
        {
            //player hurt;
            Particlles();
        }
        else
        {
            if (!dead)
            {
                Particlles();
                anim.SetTrigger("Death");


                dead = true;

                // player dead

                //Deactivate all attached component classes
                foreach (Behaviour component in components)
                {
                    component.enabled = false;
                }

            }
        }
    }

    private void Particlles()
    {
        GameObject closestPlayer = FindClosestWithTag(playerTag);
        if (closestPlayer != null)
        {
            float scaleX = closestPlayer.transform.localScale.x;
            Debug.Log("Closest Player's Scale X: " + scaleX);

            attackDirection = (closestPlayer.transform.position - transform.position).normalized;


        }
        else
        {
            Debug.Log("No enemies found!");
        }


        Quaternion spawnRotation = Quaternion.FromToRotation(Vector2.right, attackDirection);

        BleadingParticlesInstance = Instantiate(BleadingParticles, transform.position, spawnRotation);
    }

    private GameObject FindClosestWithTag(string tag)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag(tag);
        GameObject closest = null;
        float shortestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(currentPosition, player.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closest = player;
            }
        }

        return closest;
    }
}
