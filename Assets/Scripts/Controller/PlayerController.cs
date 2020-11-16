using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Luminaria;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    public struct WorldExplorationInput
    {
        public bool jump;
        public bool openInventory;
        public Vector3 move;
    }

    public struct InventoryInput
    {
        public bool closeInventory;
    }

    [SerializeField] private int playerId = 0;
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float jumpForce = 3.0f;

    private GamemodeManager gmm;
    private Rigidbody2D rb;
    private Player player;
    private ControllerMapEnabler controllerMapEnabler;

    private WorldExplorationInput we;
    private InventoryInput inv;


    private bool isGrounded = true;

    private void Awake()
    {
        gmm = GetComponent<GamemodeManager>();
        player = ReInput.players.GetPlayer(playerId);
        controllerMapEnabler = player.controllers.maps.mapEnabler;
        rb = GetComponent<Rigidbody2D>();

        foreach (ControllerMapEnabler.RuleSet rs in controllerMapEnabler.ruleSets)
            rs.enabled = false;

        controllerMapEnabler.ruleSets.Find(item => item.tag == "WorldExploration").enabled = true;
        controllerMapEnabler.Apply();
    }

    private void Start()
    {
        UpdateControllerMap("WorldExploration");
    }

    private void Update()
    {
        GetInput();
        ProcessInput();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }
    private void GetInput()
    {
        inv.closeInventory = player.GetButtonDown("Close UI");

        we.jump = player.GetButton("Jump");
        we.openInventory = player.GetButtonDown("Open UI");
        we.move.x = player.GetAxis("Move Horizontal");
        we.move = we.move.magnitude > 1.0f ? we.move.normalized : we.move;

    }

    private void ProcessInput()
    {
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

    private void MoveCharacter()
    {
        transform.position += we.move * speed * Time.deltaTime;
        if (isGrounded)
            if (we.jump)
                rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
    }

    private void UpdateControllerMap(string rsTag)
    {
        foreach (ControllerMapEnabler.RuleSet rs in controllerMapEnabler.ruleSets)
            rs.enabled = false;
        controllerMapEnabler.ruleSets.Find(item => item.tag == rsTag).enabled = true;
        controllerMapEnabler.Apply();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}