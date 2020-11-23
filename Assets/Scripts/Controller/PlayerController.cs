using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Luminaria;

public class PlayerController : MonoBehaviour
{
    public struct WorldExplorationInput
    {
        public bool jump;
        public bool openInventory;
        public float moveHorizontal;
    }

    public struct InventoryInput
    {
        public bool closeInventory;
    }

    [SerializeField] private int playerId = 0;
    [Header("External scripts")]
    [SerializeField] private GamemodeManager gmm;
    [Header("Mobility")]
    [SerializeField] public float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float wallJumpForce;
    [Header("Ground")]
    [SerializeField] Transform checkGround;
    [SerializeField] LayerMask isGround;
    [Header("Wall jump")]
    [SerializeField] Transform checkWallJump;
    [SerializeField] LayerMask isWallJump;

    Player player;
    PlayerCharacter character;
    ControllerMapEnabler controllerMapEnabler;
    WorldExplorationInput we;
    InventoryInput inv;
    bool isGrounded;
    bool isWalled;
    bool isFacingRight = true;
    bool control;
    bool platformSpell;
    const float groundRadius = .2f;
    const float wallJumpRadius = .3f;
    Vector2 m_Velocity = Vector2.zero;
    Rigidbody2D rb2D;
    Collider2D currentWall;

    private void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);
        character = GetComponent<PlayerCharacter>();
        rb2D = GetComponent<Rigidbody2D>();
        controllerMapEnabler = player.controllers.maps.mapEnabler;

        UpdateControllerMap("WorldExploration");
    }

    private void Update()
    {
        GetInputs();
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        if (control)
            rb2D.velocity = new Vector2(we.moveHorizontal * speed * Time.fixedDeltaTime * 10f, rb2D.velocity.y);
        isGrounded = isWalled = false;

        if(we.moveHorizontal > 0  && !isFacingRight)
        {
            Flip();
        }

        else if (we.moveHorizontal < 0 && isFacingRight)
        {
            Flip();
        }

        Collider2D[] groundColliders = Physics2D.OverlapCircleAll(checkGround.position, groundRadius, isGround);
        for (int i = 0; i < groundColliders.Length; i++)
            if (groundColliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                control = true;
            }

        currentWall = Physics2D.OverlapCircle(checkWallJump.position, wallJumpRadius, isWallJump);
        if (currentWall != null)
            if (currentWall.gameObject != gameObject)
                isWalled = true;
    }

    public void WallJump()
    {
        if (currentWall.transform.position.x < gameObject.transform.position.x)
            rb2D.velocity = new Vector2(wallJumpForce, wallJumpForce * 1.5f);
        else
            rb2D.velocity = new Vector2(-wallJumpForce, wallJumpForce * 1.5f);
    }

    private void GetInputs()
    {
        inv.closeInventory = player.GetButtonDown("Close UI");
        we.jump = player.GetButtonDown("Jump");
        we.openInventory = player.GetButtonDown("Open UI");
        we.moveHorizontal = player.GetAxisRaw("Move Horizontal");
    }

    private void ProcessInputs()
    {
        if (we.jump)
        {
            if (isGrounded)
            {
                isGrounded = false;
                rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            else if (isWalled)
            {
                isWalled = control = false;
                WallJump();
            }
        }
        if (we.openInventory)
        {
            UpdateControllerMap("Inventory");
            gmm.state = Gamemode.INVENTORY_OPEN;
        }
        if (inv.closeInventory)
        {
            UpdateControllerMap("WorldExploration");
            gmm.state = Gamemode.WORLD_EXPLORATION;
        }
    }

    private void UpdateControllerMap(string rsTag)
    {
        foreach (ControllerMapEnabler.RuleSet rs in controllerMapEnabler.ruleSets)
            rs.enabled = false;
        controllerMapEnabler.ruleSets.Find(item => item.tag == rsTag).enabled = true;
        controllerMapEnabler.Apply();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //Gizmos.DrawSphere(checkWallJump.position, wallJumpRadius);

        if (collision.gameObject.tag == "Ennemy")
            character.TakeDamage(1);
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        isFacingRight = !isFacingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}