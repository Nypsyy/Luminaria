using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int playerId = 0;
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float jumpForce = 3.0f;

    private Player player;
    private Rigidbody rb;
    private Vector2 move;
    private bool jump;
    private bool isGrounded = true;

    private void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);
        rb = GetComponent<Rigidbody>();

        move = Vector2.zero;
    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void GetInput()
    {
        jump = player.GetButton("Jump");
        move.x = player.GetAxis("Move Horizontal");
    }

    private void MoveCharacter()
    {
        if (isGrounded)
            if (jump)
                rb.velocity = Vector3.up * jumpForce;

        rb.MovePosition((Vector2)transform.position + (move * speed * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
