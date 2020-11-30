using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    enum SpellType
    {
        fire,
        ice,
    }
    public Transform firePoint;
    public PlayerController playerController;
    public GameObject fireSpellPrefab;
    public GameObject iceSpellPrefab;

    public GameObject abilityUI;

    Vector3 mousePosition;
    Vector3 direction;

    SpellType spellType = SpellType.fire;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!abilityUI.activeSelf)
            {
                Shoot();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            spellType = SpellType.fire;
            Debug.Log("Let's burn this place down");
        }
        
        if (Input.GetKeyDown(KeyCode.G))
        {
            spellType = SpellType.ice;
            Debug.Log("Getting cold out there..");
        }


    }

    void Shoot()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        direction = mousePosition - transform.position;

        if ((playerController.isFacingRight && direction.x < 0) || (!playerController.isFacingRight && direction.x > 0))
        {
            playerController.Flip();
        }

        switch (spellType)
        {
            case SpellType.fire:
                Instantiate(fireSpellPrefab, firePoint.position, firePoint.rotation);
                break;
            case SpellType.ice:
                Instantiate(iceSpellPrefab, firePoint.position, firePoint.rotation);
                break;
            default:

                break;
        }
        
    }
}
