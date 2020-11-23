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
        Debug.Log(healthBar.slider.value);
    }

    public void TakeDamage(int damage, bool facingRight)
    {
        if (!invincible)
        {
            controller.DoKnockback(facingRight);
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
