﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    #region Singleton

    public static PlayerCharacter instance;
    void Awake()
    {
        if (instance != null) return;
        instance = this;
    }

    #endregion 

    [SerializeField] float invincinbilityDelay;
    [SerializeField] float blinkNumber;
    [SerializeField] GameObject sprite;
    [SerializeField] Transform respawnPoint;
    [SerializeField] Vector3 scale;

    public Animator animator;
    public float maxHealth;
    public float currentHealth;
    public HealthBar healthBar;
    public bool isDead = false;

    PlayerController controller;
    bool isInvincible = false;
    float delayBetweenBlinks;

    void Start()
    {
        controller = GetComponent<PlayerController>();
        currentHealth = maxHealth;
        delayBetweenBlinks = invincinbilityDelay / (blinkNumber * 2);
        healthBar.SetMaxHealth(currentHealth);
    }

    void Update()
    {
        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("PLAYER: Died");
        StartCoroutine(Dying());
    }

    public void TakeDamage(bool kockback, float xDistance, float damage)
    {
        if (isInvincible) return;

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            return;
        }

        if (kockback)
            controller.DoKnockback(xDistance);

        StartCoroutine(Invincible());
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ennemy")
        {
            EnnemyBehavior ennemy = other.gameObject.GetComponent<EnnemyBehavior>();
            TakeDamage(true, transform.position.x - other.transform.position.x, ennemy.damage);
        }

    }

    void Respawn()
    {
        transform.position = respawnPoint.position;
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);

        isDead = false;

        animator.SetBool("IsDead", false);
    }

    IEnumerator Dying()
    {
        isDead = true;
        animator.SetBool("IsDead", true);
        yield return new WaitForSeconds(2);

        Respawn();
    }

    IEnumerator Invincible()
    {
        Debug.Log("PLAYER: Invincible");
        isInvincible = true;
        animator.SetTrigger("IsHurt");

        yield return new WaitForSeconds(.2f);

        for (float i = 0; i < invincinbilityDelay; i += delayBetweenBlinks)
        {
            if (sprite.transform.localScale != Vector3.zero)
                sprite.transform.localScale = Vector3.zero;
            else
                sprite.transform.localScale = scale;

            yield return new WaitForSeconds(delayBetweenBlinks);
        }

        Debug.Log("PLAYER: Not invincible");
        isInvincible = false;
    }

}