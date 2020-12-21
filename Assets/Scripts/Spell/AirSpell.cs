using System.Collections;
using UnityEngine;
using Rewired;
using Luminaria;

public class AirSpell : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 25f;
    public float manaCost = 0f;

    public Rigidbody2D rb;
    PlayerCharacter playerCharacter;

    Vector2 direction;
    Mouse mouse;
    Animator animator;
    Element element = Element.AIR;

    void Start() {
        animator = GetComponentInChildren<Animator>();
        mouse = ReInput.controllers.Mouse;
        playerCharacter = FindObjectOfType<PlayerCharacter>();

        direction = Camera.main.ScreenToWorldPoint(mouse.screenPosition) - transform.position;
        direction.Normalize();

        rb.velocity = direction * speed;

        AudioManager.instance.Play("AirCast");

        playerCharacter.isCasting = true;
        StartCoroutine(Travel());
    }

    void Update() {
        if (PlayerInputs.instance.releaseCast || playerCharacter.currentMana < manaCost) {
            StopCoroutine(Travel());
            StartCoroutine(Impact(false));
        }
        playerCharacter.ReduceMana(manaCost);
        direction = Camera.main.ScreenToWorldPoint(mouse.screenPosition) - transform.position;
        rb.velocity = direction * speed;
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Ennemy") {
            EnnemyBehavior ennemy = other.gameObject.GetComponent<EnnemyBehavior>();
            ennemy.TakeDamage(damage, element);
        }
    }

    IEnumerator Travel() {
        yield return new WaitForSeconds(3);
        StartCoroutine(Impact(true));
    }

    IEnumerator Impact(bool sound) {
        if (sound)
            AudioManager.instance.Play("AirImpact");

        animator.SetTrigger("IsDestroyed");
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(.2f);
        playerCharacter.isCasting = false;
        Destroy(gameObject);
    }
}
