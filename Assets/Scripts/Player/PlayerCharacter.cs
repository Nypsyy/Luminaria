﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCharacter : MonoBehaviour
{
    #region Singleton

    public static PlayerCharacter instance;
    void Awake() {
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
    public float maxMana;
    public float currentMana;
    public ManaBar manaBar;
    public float manaRegeneration;

    public bool canCastSpells = true;
    public bool isCasting = false;

    public UnityEvent OnRespawnEvent;
    public UnityEvent OnDeathEvent;

    PlayerController controller;
    bool isInvincible = false;
    float delayBetweenBlinks;

    void Start() {
        controller = GetComponent<PlayerController>();
        delayBetweenBlinks = invincinbilityDelay / (blinkNumber * 2);

        currentHealth = maxHealth;
        currentMana = maxMana;
        healthBar.SetMaxHealth(maxHealth);
        manaBar.SetMaxMana(maxMana);

        if (OnRespawnEvent == null)
            OnRespawnEvent = new UnityEvent();
        if (OnDeathEvent == null)
            OnDeathEvent = new UnityEvent();
    }

    void Update() {
        if (currentHealth <= 0)
            Die();
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        if (currentMana <= 10f && canCastSpells) {
            StartCoroutine(OutOfMana());
        }
        if (currentMana > maxMana)
            currentMana = maxMana;

        if (!canCastSpells)
            GetComponentInChildren<SpriteRenderer>().color = Color.blue;
        else
            GetComponentInChildren<SpriteRenderer>().color = Color.white;

        if (currentMana < maxMana && !isCasting)
            AugmentMana(manaRegeneration);
    }

    void Die() {
        Debug.Log("PLAYER: Died");
        StartCoroutine(Dying());
        OnDeathEvent.Invoke();
    }

    public void TakeDamage(bool kockback, float xDistance, float damage) {
        if (isInvincible) return;

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0) {
            currentHealth = 0;
            return;
        }

        if (kockback)
            controller.DoKnockback(xDistance);

        StartCoroutine(Invincible());
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Ennemy") {
            EnnemyBehavior ennemy = other.gameObject.GetComponent<EnnemyBehavior>();
            TakeDamage(true, transform.position.x - other.transform.position.x, ennemy.damage);
        }

    }

    void Respawn() {
        animator.SetBool("IsDead", false);
        transform.position = respawnPoint.position;
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
        canCastSpells = true;

        OnRespawnEvent.Invoke();
    }

    public void ReduceMana(float amount) {
        currentMana -= amount;
        manaBar.SetMana(currentMana);
    }

    public void AugmentMana(float amount) {
        currentMana += amount;
        manaBar.SetMana(currentMana);
    }

    IEnumerator Dying() {
        animator.SetBool("IsDead", true);
        canCastSpells = false;
        yield return new WaitForSeconds(2);
        Respawn();
    }

    IEnumerator Invincible() {
        Debug.Log("PLAYER: Invincible");
        isInvincible = true;
        animator.SetTrigger("IsHurt");

        yield return new WaitForSeconds(.2f);

        for (float i = 0; i < invincinbilityDelay; i += delayBetweenBlinks) {
            if (sprite.transform.localScale != Vector3.zero)
                sprite.transform.localScale = Vector3.zero;
            else
                sprite.transform.localScale = scale;

            yield return new WaitForSeconds(delayBetweenBlinks);
        }

        Debug.Log("PLAYER: Not invincible");
        isInvincible = false;
    }

    IEnumerator OutOfMana() {
        canCastSpells = false;
        yield return new WaitUntil(() => currentMana == maxMana);
        canCastSpells = true;
    }
}
