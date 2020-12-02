using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class FireSpell : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 25f;

    public Rigidbody2D rb;
    public GameObject impactEffet;

    Mouse mouse;
    Vector2 direction;

    void Start()
    {
        mouse = ReInput.controllers.Mouse;

        direction = Camera.main.ScreenToWorldPoint(mouse.screenPosition) - transform.position;
        rb.velocity = direction.normalized * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ennemy")
        {
            EnnemyBehavior ennemy = other.gameObject.GetComponent<EnnemyBehavior>();
            ennemy.TakeDamage(damage);

            Instantiate(impactEffet, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
