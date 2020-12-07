using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class FireSpell : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 25f;

    public Rigidbody2D rb;

    Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        Mouse mouse = ReInput.controllers.Mouse;

        Vector2 direction = Camera.main.ScreenToWorldPoint(mouse.screenPosition) - transform.position;
        direction.Normalize();

        rb.velocity = direction * speed;

        AudioManager.instance.Play("FireCast");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ennemy")
        {
            EnnemyBehavior ennemy = other.gameObject.GetComponent<EnnemyBehavior>();
            ennemy.TakeDamage(damage);

            StartCoroutine(Impact(true));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            StartCoroutine(Impact(false));
    }

    IEnumerator Impact(bool sound)
    {
        if (sound)
            AudioManager.instance.Play("FireImpact");

        animator.SetTrigger("IsDestroyed");
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(.2f);
        Destroy(gameObject);
    }
}
