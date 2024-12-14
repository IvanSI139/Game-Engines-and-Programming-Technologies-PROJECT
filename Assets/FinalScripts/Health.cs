using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;//animator
    private bool dead;

    [Header("iFrames")]
    //when hurt

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    [SerializeField] private ParticleSystem BleadingParticles;
    private Vector2 attackDirection;
    public string enemyTag = "Enemy";

    private ParticleSystem BleadingParticlesInstance;
        
    private void Awake()
    {
        GameManeger.Instance.Health = this;
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        dead = false;
    }

    private void Update()
    {
        if (dead && currentHealth > 0) 
        {
            dead = false;

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

    public void Heal(float amount)
    {
        if (dead) return; // Prevent healing if the player is dead.
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, startingHealth);
        Debug.Log($"Healed by {amount}. Current health: {currentHealth}");
    }

    private void Particlles()
    {

        GameObject closestEnemy = FindClosestWithTag(enemyTag);
        if (closestEnemy != null)
        {
            float scaleX = closestEnemy.transform.localScale.x;
            Debug.Log("Closest Enemy's Scale X: " + scaleX);
            if (scaleX < 0) 
            {
                attackDirection = (closestEnemy.transform.position - transform.position).normalized;
            }

        }
        else
        {
            Debug.Log("No enemies found!");
        }

    Quaternion spawnRotation = Quaternion.FromToRotation(Vector2.right, attackDirection);

    BleadingParticlesInstance = Instantiate(BleadingParticles,transform.position, spawnRotation);
    }


    private GameObject FindClosestWithTag(string tag)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
        GameObject closest = null;
        float shortestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(currentPosition, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closest = enemy;
            }
        }

        return closest;
    }
    public void Save(ref PlayerHealtData data)
    {
        data.currentHealth = currentHealth;
    }

    public void Load(PlayerHealtData data)
    {
        currentHealth = data.currentHealth;
    }
}

[System.Serializable]

public struct PlayerHealtData
{
    public float currentHealth;
}
