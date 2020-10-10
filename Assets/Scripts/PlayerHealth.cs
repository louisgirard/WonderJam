﻿using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 50f;
    float health;
    HealthBar healthBar;

    private void Start()
    {
        health = maxHealth;
        healthBar = FindObjectOfType<HealthBar>();
        healthBar.UpdateSlider(health, maxHealth);
    }

    public void TakeDamage(float damage)
    {
        health = Mathf.Max(health - damage, 0);
        healthBar.UpdateSlider(health, maxHealth);
        if(health == 0)
        {
            print("dead");
        }
        print("health = " + health);
    }
}