﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Luminaria;

public class Spell : MonoBehaviour
{
    Element elementType = Element.WATER;

    public Transform firePoint;
    public PlayerController playerController;
    public PlayerCharacter playerCharacter;
    public GameObject fireSpellPrefab;
    public GameObject iceSpellPrefab;
    public GameObject earthSpellPrefab;
    public GameObject airSpellPrefab;

    public bool earthPlatformExists = false;

    public GameObject abilityUI;

    [SerializeField] ElementWheelBehavior elementWheel;

    Animator animator;
    PlayerController controller;
    Mouse mouse;

    void Start() {
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<PlayerController>();
        mouse = ReInput.controllers.Mouse;
    }

    void Update() {
        if (PlayerInputs.instance.closeWheel) {
            UpdateElementType();
        }

        if (PlayerInputs.instance.castSpell) {
            if (!abilityUI.activeSelf) {
                if (playerCharacter.canCastSpells)
                    Shoot();
            }
        } else if (PlayerInputs.instance.releaseCast) {
            StopCoroutine(SummoningEarth());
            controller.canControl = true;
        }
    }

    void Shoot() {
        Vector2 direction = Camera.main.ScreenToWorldPoint(mouse.screenPosition) - transform.position;
        direction.Normalize();

        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


        if ((playerController.isFacingRight && direction.x < 0) || (!playerController.isFacingRight && direction.x > 0)) {
            playerController.Flip();
        }

        switch (elementType) {
            case Element.FIRE:
                if (fireSpellPrefab.GetComponent<FireSpell>().manaCost < playerCharacter.currentMana) {
                    animator.SetTrigger("Attack");
                    Instantiate(fireSpellPrefab, firePoint.position, Quaternion.Euler(0f, 0f, rotation + 90));
                }
                break;

            case Element.WATER:
                if (iceSpellPrefab.GetComponent<IceSpell>().manaCost < playerCharacter.currentMana) {
                    animator.SetTrigger("Attack");
                    Instantiate(iceSpellPrefab, firePoint.position, Quaternion.Euler(0f, 0f, rotation - 90));
                }
                break;

            case Element.EARTH:
                if (earthSpellPrefab.GetComponent<EarthSpell>().manaCost < playerCharacter.currentMana)
                    if (!earthPlatformExists)
                        StartCoroutine(SummoningEarth());
                break;

            case Element.AIR:
                if (airSpellPrefab.GetComponent<AirSpell>().manaCost < playerCharacter.currentMana) {
                    animator.SetTrigger("Attack");
                    Instantiate(airSpellPrefab, firePoint.position, Quaternion.identity);
                }
                break;
        }
    }

    void UpdateElementType() {
        switch (elementWheel.selection) {
            case 0:
                elementType = Element.WATER;
                break;
            case 1:
                elementType = Element.EARTH;
                break;
            case 2:
                elementType = Element.FIRE;
                break;
            case 3:
                elementType = Element.AIR;
                break;
        }

    }

    IEnumerator SummoningEarth() {
        earthPlatformExists = true;
        controller.canControl = false;
        controller.StopMotion();
        yield return new WaitUntil(() => PlayerInputs.instance.summonSpell);
        Instantiate(earthSpellPrefab, Camera.main.ScreenToWorldPoint(mouse.screenPosition), Quaternion.identity);
        controller.canControl = true;
    }

    void oom() {
        animator.SetTrigger("OutOfMana");
    }
}
