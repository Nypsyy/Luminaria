﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminaria;

public class EarthSpell : MonoBehaviour
{
    public GameObject sprite;
    public Spell spellManager;
    public float manaCost = 0f;

    PlayerCharacter playerCharacter;

    Element element = Element.EARTH;

    void Start() {
        spellManager = FindObjectOfType<Spell>();
        playerCharacter = FindObjectOfType<PlayerCharacter>();

        playerCharacter.ReduceMana(manaCost);
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            StartCoroutine(WaitUntilDestroy());
            StartCoroutine(Shaking());
        }
    }

    IEnumerator WaitUntilDestroy() {
        yield return new WaitForSeconds(1.6f);
        spellManager.earthPlatformExists = false;
        Destroy(gameObject);
    }

    IEnumerator Shaking() {
        yield return new WaitForSeconds(.6f);

        for (float i = 0; i < 1; i += .08f) {
            float x = Random.Range(-.05f, .05f);
            float y = Random.Range(-.05f, .05f);

            sprite.transform.position += new Vector3(x, y, 0);

            yield return new WaitForSeconds(.08f);
            sprite.transform.position -= new Vector3(x, y, 0);
        }
    }
}

