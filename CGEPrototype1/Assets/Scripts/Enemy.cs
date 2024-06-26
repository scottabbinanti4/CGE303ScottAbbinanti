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

    public int damage = 10;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            if(playerHealth == null)
            {
                Debug.LogError("PlayerHealth script not found on player");
                return;
            }

            playerHealth.TakeDamage(damage);

            playerHealth.Knockback(transform.position);

        }
    }

}
