using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BossBar : MonoBehaviour
{
    [SerializeField] private EnemyHealth bossHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;
    private EnemyHealth EnemyHealth;

    private void Start()
    {
        totalHealthBar.fillAmount = bossHealth.currentHealth / 10;
    }
    private void Update()
    {
        currentHealthBar.fillAmount = bossHealth.currentHealth / 10;
    }

}
