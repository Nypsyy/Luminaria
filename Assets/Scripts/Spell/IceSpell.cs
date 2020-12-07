using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Luminaria;

public class IceSpell : MonoBehaviour
{
    public float speed = 2f;
    public float damage = 50f;

    public Rigidbody2D rb;

    Animator animator;
    Element element = Element.WATER;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        Mouse mouse = ReInput.controllers.Mouse;

        Vector2 direction = Camera.main.ScreenToWorldPoint(mouse.screenPosition) - transform.position;
        direction.Normalize();
        rb.velocity = direction * speed;

        AudioManager.instance.Play("IceCast");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            StartCoroutine(Impact(false));
        if (other.gameObject.tag == "Ennemy")
        {
            EnnemyBehavior ennemy = other.gameObject.GetComponent<EnnemyBehavior>();
            ennemy.TakeDamage(damage, element);

            StartCoroutine(Impact(true));
        }
    }

    IEnumerator Impact(bool sound)
    {
        if (sound)
            AudioManager.instance.Play("IceImpact");

        animator.SetTrigger("IsDestroyed");
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(.2f);
        Destroy(gameObject);
    }
}

