using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PressurePlateBehavior : MonoBehaviour
{
    [SerializeField] BoxCollider2D falseGround;
    [SerializeField] GameObject trap;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            falseGround.enabled = false;
            StartCoroutine(DestroyTrap(trap));
        }
    }

    IEnumerator DestroyTrap(GameObject obj)
    {
        yield return new WaitForSeconds(2);
        Destroy(obj);
    }
}
