using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    [Header("Object references")]
    public Rigidbody2D rb;
    public GrapplingRope rope;
    public LogicScript logic;
    public TextMeshProUGUI lifeCountText;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    [Header("Life variables")]
    public Vector2 spawnPoint = new Vector2(0, 0);
    public float deathFloorHeight = -6;
    public IntegerValue lifeCount; 
    public bool playerIsAlive = true;

    [Header("Movement Variables")]
    [SerializeField] private float movementAcceleration = 25f;
    [SerializeField] private float airAcceleration = 15f;
    [SerializeField] private float maxMoveSpeed = 5f;
    [SerializeField] private float groundLinearDrag = 4f;
    private bool isGrounded;
    private float horizontalDirection;
    private bool changingDirection => (rb.velocity.x > 0f && horizontalDirection < 0f) || (rb.velocity.x < 0f && horizontalDirection > 0f);
    private bool hasGrappled = false;

    [Header("Jump Variables")]
    [SerializeField] private float jumpStrength = 10f;
    [SerializeField] private float airLinearDrag = 1f;
    [SerializeField] private float fallMultiplier = 4f;
    [SerializeField] private float lowJumpFallMultiplier = 7f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rope.enabled = false;
    }
    void OnCollisionStay2D()
    {
        isGrounded = true;
        animator.SetBool("Is_grounded", true);
    }
    void OnCollisionExit2D()
    {
        isGrounded = false;
        animator.SetBool("Is_grounded", false);
    }

    void Update()
    {
        spriteRenderer.flipX = rb.velocity.x < 0f;
        if (playerIsAlive)
        {
            horizontalDirection = GetInput().x;
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.velocity += Vector2.up * jumpStrength;
                isGrounded = false;
            }
        }

        if(transform.position.y < deathFloorHeight)
        {
            playerDeath();
        }
    }

    private void FixedUpdate()
    {
        
        animator.SetFloat("Vertical_velocity", rb.velocity.y);
        animator.SetBool("Is_grappling", rope.isGrappling);

        if (playerIsAlive)
        {
            MoveCharacter();
            if (isGrounded)
            {
                hasGrappled = false;
                ApplyGroundLinearDrag();
            }
            else
            {
                ApplyAirLinearDrag();
                FallMultiplier();
            }
        }

    }

    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void MoveCharacter()
    {
        //Movement stuff
        if (isGrounded)
        {
            animator.SetFloat("Horizontal_velocity", Mathf.Abs(rb.velocity.x));

            rb.AddForce(new Vector2(horizontalDirection, 0f) * movementAcceleration);        
            if (Mathf.Abs(rb.velocity.x) > maxMoveSpeed)
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxMoveSpeed, rb.velocity.y);
            }
            
        }
        else
        {
            rb.AddForce(new Vector2(horizontalDirection, 0f) * airAcceleration);
        }
    }

    private void ApplyGroundLinearDrag()
    {
        if(Mathf.Abs(horizontalDirection) < 0.4f || changingDirection)
        {
            rb.drag = groundLinearDrag;
        }
        else
        {
            rb.drag = 0f;
        }
    }

    private void ApplyAirLinearDrag()
    {
        if (rope.isGrappling)
        {
            hasGrappled = true;
            rb.drag = 0f;
        }
        else
        {
            rb.drag = airLinearDrag;
        }     
    }

    private void FallMultiplier()
    {
        if (!rope.isGrappling)
        {
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = fallMultiplier;
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump") && !hasGrappled)
            {
                rb.gravityScale = lowJumpFallMultiplier;
            }
            else
            {
                rb.gravityScale = 2f;
            }
        }
        else
        {
            rb.gravityScale = 2f;
        }
    }

    public void playerDeath()
    {
        foreach (GameObject collectable in logic.inactiveObjects)
        {
            collectable.SetActive(true);
        }
        logic.inactiveObjects.Clear();

        logic.exitDoor.SetActive(false);
        logic.collectedGoalCount = 0;
        logic.goalCountText.text = logic.collectedGoalCount + "/" + logic.levelGoalCount;
        lifeCount.initialValue -= 1;
        lifeCountText.text = " Lives: " + lifeCount.initialValue;
        rb.velocity = new Vector2(0, 0);
        transform.position = spawnPoint;
        if (lifeCount.initialValue > 0)
        {
            StartCoroutine(Respawn());
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator FadeOut()
    {    
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFade>().StartFadeOut();
        yield return new WaitForSeconds(1);
    }

    IEnumerator Respawn()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFade>().StartFadeIn();
        yield return new WaitForSeconds(1);
    }
}
