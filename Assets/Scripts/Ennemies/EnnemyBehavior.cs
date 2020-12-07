using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Luminaria;

public class EnnemyBehavior : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject deathEffect;

    public EnnemyData ennemyData;
    public EnnemyHealthBar healthBar;

    AIPath aiPath;
    AIDestinationSetter dest;

    public string ennemyName;
    public float health;
    public float maxHealth;
    public float speed;
    public float damage;
    public Element element;
    public float aggroRange;

    Rigidbody2D rb2D;
    Animator animator;
    bool isInvincible;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        aiPath = GetComponent<AIPath>();
        dest = GetComponent<AIDestinationSetter>();

        if (ennemyData != null)
        {
            LoadEnnemy();
            healthBar.SetHealth(maxHealth, maxHealth);

            if (aiPath != null)
            {
                aiPath.canSearch = false;
                aiPath.canMove = false;
                aiPath.maxSpeed = speed;
            }

            if (dest != null)
                dest.target = player.transform;
        }
    }

    void Update()
    {
        animator.SetFloat("Move", rb2D.velocity.x);

        if (Vector2.Distance(transform.position, player.transform.position) < aggroRange)
        {
            aiPath.canSearch = true;
            aiPath.canMove = true;
        }
        else
        {
            aiPath.canSearch = false;
            aiPath.canMove = false;
        }
    }

    void LoadEnnemy()
    {
        GameObject sprite = Instantiate(ennemyData.ennemyModel);
        sprite.transform.SetParent(transform);
        sprite.transform.localPosition = Vector3.zero;
        sprite.transform.rotation = Quaternion.identity;
        animator = sprite.GetComponent<Animator>();

        ennemyName = ennemyData.ennemyName;
        health = maxHealth = ennemyData.health;
        speed = ennemyData.speed;
        damage = ennemyData.damage;
        element = ennemyData.element;
    }

    public void TakeDamage(float damage, Element e)
    {
        if (isInvincible || e == element) return;

        health -= damage;
        healthBar.SetHealth(health, maxHealth);

        if (health <= 0)
        {
            Debug.Log("ENNEMY: Die");
            health = 0;
            Die();
        }

        StartCoroutine(Invincible());
    }

    void Die()
    {
        animator.SetTrigger("IsDead");
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    IEnumerator Invincible()
    {
        isInvincible = true;
        SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.color = Color.red;
        animator.SetTrigger("IsHurt");
        yield return new WaitForSeconds(.06f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(.06f);
        isInvincible = false;
    }
}
