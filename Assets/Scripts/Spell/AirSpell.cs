using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class AirSpell : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 25f;

    public Rigidbody2D rb;

    Vector2 direction;
    Mouse mouse;
    Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        mouse = ReInput.controllers.Mouse;

        direction = Camera.main.ScreenToWorldPoint(mouse.screenPosition) - transform.position;
        direction.Normalize();

        rb.velocity = direction * speed;

        AudioManager.instance.Play("AirCast");

        StartCoroutine(Travel());
    }

    void Update()
    {
        if (PlayerInputs.instance.releaseCast)
        {
            StopCoroutine(Travel());
            StartCoroutine(Impact(false));
        }
        direction = Camera.main.ScreenToWorldPoint(mouse.screenPosition) - transform.position;
        rb.velocity = direction * speed;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ennemy")
        {
            EnnemyBehavior ennemy = other.gameObject.GetComponent<EnnemyBehavior>();
            ennemy.TakeDamage(damage);
        }
    }

    IEnumerator Travel()
    {
        Debug.Log("Oui");
        yield return new WaitForSeconds(3);
        StartCoroutine(Impact(true));
    }

    IEnumerator Impact(bool sound)
    {
        if (sound)
            AudioManager.instance.Play("AirImpact");

        animator.SetTrigger("IsDestroyed");
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(.2f);
        Destroy(gameObject);
    }
}
