using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerCharacter : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public HealthBar healthBar;

    PlayerController controller;
    bool invincible;

    void Start()
    {
        controller = GetComponent<PlayerController>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(currentHealth);
    }

    void Update()
    {
        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("T'es mort");
    }


    public void TakeDamage(int damage)
    {
        if (!invincible)
        {
            controller.DoKnockback();
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            StartCoroutine(Invincible());
        }
    }

    IEnumerator Invincible()
    {
        invincible = true;
        yield return new WaitForSeconds(1);
        invincible = false;
    }


}
