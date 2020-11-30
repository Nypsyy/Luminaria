using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Rewired;
using Luminaria;

public class PlayerController : MonoBehaviour
{
    struct WorldExplorationInput
    {
        public bool jump;
        public bool openInventory;
        public float moveHorizontal;
    }

    struct InventoryInput
    {
        public bool closeInventory;
    }

    [SerializeField] private int playerId = 0;
    [Header("Interact")]
    [SerializeField] public GameObject interactUI;
    [SerializeField] public TextMeshProUGUI text;
    [Header("Mobility")]
    [SerializeField] public float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float wallJumpForce;
    [SerializeField] float knockbackForce;

    [Header("Ground")]
    [SerializeField] Transform checkGround;
    [SerializeField] LayerMask isGround;
    [SerializeField] LayerMask isTrap;
    [Header("Wall jump")]
    [SerializeField] Transform checkWallJump;
    [SerializeField] LayerMask isWallJump;

    public bool isFacingRight = true;

    Player player;
    PlayerCharacter character;
    ControllerMapEnabler controllerMapEnabler;
    WorldExplorationInput we;
    InventoryInput inv;
    bool isGrounded;
    bool isWalled;
    bool canControl = true;
    const float groundRadius = .2f;
    const float wallJumpRadius = .3f;
    Rigidbody2D rb2D;
    Collider2D currentWall;

    void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);
        character = GetComponent<PlayerCharacter>();
        rb2D = GetComponent<Rigidbody2D>();
        controllerMapEnabler = player.controllers.maps.mapEnabler;

        UpdateControllerMap("WorldExploration");
    }

    void Update()
    {
        GetInputs();
        ProcessInputs();
    }

    void FixedUpdate()
    {
        if (character.isDead)
        {
            canControl = false;
            rb2D.velocity = Vector2.zero;
            return;
        }

        isGrounded = isWalled = false;

        if (canControl)
            rb2D.velocity = new Vector2(we.moveHorizontal * speed * Time.fixedDeltaTime * 10f, rb2D.velocity.y);

        if (we.moveHorizontal > 0 && !isFacingRight)
            Flip();
        else if (we.moveHorizontal < 0 && isFacingRight)
            Flip();

        if (CheckOverlap(checkGround.position, groundRadius, isGround))
        {
            isGrounded = true;
        }
        if (CheckOverlap(checkWallJump.position, wallJumpRadius, isWallJump))
        {
            isWalled = true;
        }
    }

    bool CheckOverlap(Vector3 position, float radius, LayerMask layerMask)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radius, layerMask);
        for (int i = 0; i < colliders.Length; i++)
            if (colliders[i].gameObject != gameObject)
                return true;
        return false;
    }

    void WallJump()
    {
        isWalled = false;
        StartCoroutine(LoseControl());

        if (currentWall.transform.position.x < gameObject.transform.position.x)
            rb2D.velocity = new Vector2(wallJumpForce, wallJumpForce * 1.5f);
        else
            rb2D.velocity = new Vector2(-wallJumpForce, wallJumpForce * 1.5f);
    }

    void Jump()
    {
        isGrounded = false;
        rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void DoKnockback(float xDistance)
    {
        StartCoroutine(LoseControl());
        rb2D.velocity = new Vector2(xDistance * knockbackForce, 4f);
    }

    void GetInputs()
    {
        inv.closeInventory = player.GetButtonDown("Close UI");
        we.jump = player.GetButtonDown("Jump");
        we.openInventory = player.GetButtonDown("Open UI");
        we.moveHorizontal = player.GetAxisRaw("Move Horizontal");
    }

    void ProcessInputs()
    {
        if (we.jump)
        {
            if (isGrounded)
                Jump();
            else if (isWalled)
                WallJump();
        }

        if (we.openInventory)
        {
            UpdateControllerMap("Inventory");
            GamemodeManager.instance.state = Gamemode.INVENTORY_OPEN;
        }
        if (inv.closeInventory)
        {
            UpdateControllerMap("WorldExploration");
            GamemodeManager.instance.state = Gamemode.WORLD_EXPLORATION;
        }
    }

    void UpdateControllerMap(string rsTag)
    {
        foreach (ControllerMapEnabler.RuleSet rs in controllerMapEnabler.ruleSets)
            rs.enabled = false;
        controllerMapEnabler.ruleSets.Find(item => item.tag == rsTag).enabled = true;
        controllerMapEnabler.Apply();
    }

    public void Flip()
    {
        // Switch the way the player is labelled as facing.
        isFacingRight = !isFacingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "ItemPickUp":
                {
                    text.text = "E - Pick up";
                    Debug.Log("Item");

                    interactUI.SetActive(true);
                }
                break;

            case "Vendor":
                {
                    text.text = "E - Shop";
                    Debug.Log("Vendor");
                    interactUI.SetActive(true);
                }
                break;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        interactUI.SetActive(false);
    }

    public IEnumerator LoseControl()
    {
        Debug.Log("CONTROLLER: Lost control");
        canControl = false;
        yield return new WaitForSeconds(.4f);
        Debug.Log("CONTROLLER: Recovered control");
        canControl = true;
    }
}