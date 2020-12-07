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

    PlayerInputs inputs;
    Vector3 mousePosition;
    Vector2 direction;
    SpellType spellType = SpellType.fire;

    void Awake()
    {
        inputs = GetComponent<PlayerInputs>();
        if (inputs == null) throw new System.Exception("No inputs detected");
    }

    // Update is called once per frame
    void Update()
    {
        if (inputs.castSpell)
        {
            if (!abilityUI.activeSelf)
            {
                //Shoot();
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
        direction = inputs.mouse.screenPosition - (Vector2)transform.position;

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
        }
    }
}
