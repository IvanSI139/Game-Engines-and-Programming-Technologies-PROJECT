using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BossBar2 : MonoBehaviour
{
    [SerializeField] private EnemyHealth bossHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;
    private EnemyHealth EnemyHealth;

    private void Start()
    {
    }
    private void Update()
    {
        currentHealthBar.fillAmount = Mathf.Clamp01(bossHealth.currentHealth / 20f);

        //Debug.Log($"Current Health: {bossHealth.currentHealth}, Fill Amount: {currentHealthBar.fillAmount}");
    }

}
