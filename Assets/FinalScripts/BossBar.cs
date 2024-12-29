using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBar : MonoBehaviour
{
    [SerializeField] private Health bossHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    private void Start()
    {
        totalHealthBar.fillAmount = bossHealth.currentHealth / 10;
    }
    private void Update()
    {
        currentHealthBar.fillAmount = bossHealth.currentHealth / 10;
    }
}
