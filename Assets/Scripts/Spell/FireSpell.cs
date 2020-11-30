using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpell : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 25;

    public Rigidbody2D rb;
    public GameObject impactEffet;

    Vector3 mousePosition;
    Vector2 direction;

    void Start()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        direction = mousePosition - transform.position;
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
