using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;
    
    
    public float startHealth = 100;
    private float health; 
    
    public int value = 50;

    
    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;
    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }
    
    
    void Die()
    {
        PlayerStats.Money += value;
        
       GameObject effect = (GameObject) Instantiate(deathEffect, transform.position, Quaternion.identity);
       Destroy(effect, 5f);
        
         
        Destroy(gameObject);
    }

    
}
