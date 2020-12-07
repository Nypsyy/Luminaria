using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
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

    PlayerCharacter character;
    bool isGrounded;
    bool isWalled;
    bool inDialogue = false;
    bool interact = false;
    bool canControl = true;
    bool nextDialogue = false;
    const float groundRadius = .2f;
    const float wallJumpRadius = .3f;
    Rigidbody2D rb2D;
    Collider2D currentWall;

    void Awake()
    {
        PlayerInputs.instance.UpdateControllerMap("World Exploration");

        character = GetComponent<PlayerCharacter>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (PlayerInputs.instance.jump)
        {
            if (isGrounded)
                Jump();
            else if (isWalled)
                WallJump();
        }

        if (Input.GetMouseButtonDown(0) && inDialogue) nextDialogue = true;
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
            rb2D.velocity = new Vector2(PlayerInputs.instance.moveHorizontal * speed * Time.fixedDeltaTime * 10f, rb2D.velocity.y);

        if (PlayerInputs.instance.moveHorizontal > 0 && !isFacingRight)
            Flip();
        else if (PlayerInputs.instance.moveHorizontal < 0 && isFacingRight)
            Flip();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(checkGround.position, groundRadius, isGround);
        for (int i = 0; i < colliders.Length; i++)
            if (colliders[i].gameObject != gameObject)
                isGrounded = true;

        currentWall = Physics2D.OverlapCircle(checkWallJump.position, wallJumpRadius, isWallJump);
        if (currentWall != null && currentWall.gameObject != gameObject)
            isWalled = true;

        
        
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

    public void Flip()
    {
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
                    interactUI.SetActive(true);
                }
                break;

            case "Vendor":
                {
                    text.text = "E - Shop";
                    interactUI.SetActive(true);
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Dialogue":
                {
                    if (!inDialogue)
                    {
                        Dialogues dialogue = other.gameObject.GetComponent<Dialogues>();
                        inDialogue = true;

                        dialogue.InitDialogue();
                        dialogue.LoadDialogue();
                    }
                }
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Dialogue":
                {
                    Dialogues dialogue = other.gameObject.GetComponent<Dialogues>();

                    if(nextDialogue)
                    {
                        dialogue.nextDialogue();
                        nextDialogue = false;
                    }
                }
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Dialogue":
                {
                    Transform dialoguePoint = gameObject.transform.Find("/" + other.name + "/DialoguePoint");
                    foreach (Transform child in dialoguePoint)
                    {
                        GameObject.Destroy(child.gameObject);
                    }

                    Dialogues dialogue = other.gameObject.GetComponent<Dialogues>();
                    

                    Transform bubble = gameObject.transform.Find("/" + other.name + "/Bubble");
                    if(bubble != null)  bubble.gameObject.SetActive(false);
                    
                    Transform respBubble = gameObject.transform.Find("/" + other.name + "/RespBubble");
                    if(bubble != null) respBubble.gameObject.SetActive(false);

                    dialogue.InitDialogue();
                    inDialogue = false;
                }
                break;
        }
    }

    void OnCollisionExit2D(Collision2D other)
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