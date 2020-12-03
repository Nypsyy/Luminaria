using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class IceSpell : MonoBehaviour
{
    public float speed = 5f;
    public float damage = 50f;

    public Rigidbody2D rb;
    public GameObject impactEffet;

    Mouse mouse;
    Vector3 direction;

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

