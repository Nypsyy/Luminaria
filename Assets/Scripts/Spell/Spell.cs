using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public Transform firePoint;
    public PlayerController playerController;
    public GameObject fireSpellPrefab;

    Vector3 mousePosition;
    Vector3 direction;

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        Debug.Log(transform.forward);
    }

    void Shoot()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        direction = mousePosition - transform.position;

        if((playerController.isFacingRight && direction.x < 0) || (!playerController.isFacingRight && direction.x > 0))
        {
            playerController.Flip();
        }

        Instantiate(fireSpellPrefab, firePoint.position, firePoint.rotation);
    }
}
