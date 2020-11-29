using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpell : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 50;

    public Rigidbody2D rb;
    public GameObject impactEffet;

    Vector3 mousePosition;
    Vector3 direction;

    void Start()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        direction = mousePosition - transform.position;
        rb.velocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);

        Dummy dummy = collision.GetComponent<Dummy>();
        if (dummy != null)
        {
            dummy.TakeDamage(damage);

        }

        Instantiate(impactEffet, transform.position, transform.rotation);
        Destroy(gameObject);
    }


}

