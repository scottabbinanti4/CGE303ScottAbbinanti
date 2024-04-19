using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //the enemy's health
    public int health = 100;

    //a prefab to spawn when the enemy dies
    public GameObject deathEffect;

    private DisplayBar healthBar;

    private void Start()
    {
        healthBar = GetComponentInChildren<DisplayBar>();

        if (healthBar == null)
        {
            Debug.LogError("HealthBar (DisplayBar script) was not found");
            return;
        }

        healthBar.SetMaxValue(health);
    }

    //a function to take damage
    public void TakeDamage(int damage)
    {
        health -= damage;

        healthBar.SetValue(health);

        if (health <= 0)
        {
            Die();
        }
    }

    //a function to die
    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

}
