using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] Transform respawnPoint;

    void Start()
    {
        transform.position = respawnPoint.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Trap")
            transform.position = respawnPoint.transform.position;
    }
}
