using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
