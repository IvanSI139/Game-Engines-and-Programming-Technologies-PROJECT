using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
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
        }
        else
        {
            if (!dead)
            {
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

    public void Save(ref EnemyrHealtData data)
    {
        data.EnemyHealth = currentHealth;

        Debug.Log($"Saving EnemyHealth: {data.EnemyHealth}");
    }

    public void Load(EnemyrHealtData data)
    {
        currentHealth = data.EnemyHealth;

        Debug.Log($"Loaded EnemyHealth: {data.EnemyHealth}");
    }
}

[System.Serializable]

public struct EnemyrHealtData
{
    public float EnemyHealth;
}
