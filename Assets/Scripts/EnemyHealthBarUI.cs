using System;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarUI : MonoBehaviour
{
    [SerializeField] private Image healthBarImage;
    
    private IEnemy enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<IEnemy>();
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthBarImage.fillAmount = enemy.GetEnemyHealthPercentage();
    }
    
}
