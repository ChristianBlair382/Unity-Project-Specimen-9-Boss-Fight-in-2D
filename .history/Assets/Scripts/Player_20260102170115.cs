using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerPhysicalState
{
    Grounded = 0,
    Airborne = 1
}
enum PlayerInteractionState
{
    Idle = 0,
    Walking = 1,
    Jumping = 2,
    Attacking = 3,
    Hurt = 4
}

public class Player : MonoBehaviour
{
    // Player Statistics
    [SerializeField] PlayerInteractionState interactionState = PlayerInteractionState.Idle;
    [SerializeField] PlayerPhysicalState physicalState = PlayerPhysicalState.Airborne;
    private float Horizontal_SPD = 3f;
    private float Vertical_SPD = 5f;
    private int HP = 6;
    private int ATK_Power = 1;
    public Vector2 ATK_Size;

    // Unity Components
    private Rigidbody2D rb;
    private Animator anim;
    public GameObject ATKHitBoxInstance;
    public LayerMask enemies;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Handle Movement
        MoveHorizontal();

        Jump();

        // Handle Attacking
        if(Input.GetButtonDown("Fire1") && (interactionState != PlayerInteractionState.Attacking || interactionState != PlayerInteractionState.Jumping) && physicalState == PlayerPhysicalState.Grounded)
        {
            interactionState = PlayerInteractionState.Attacking;
        }

        AnimatorUpdater();
    }

    // Update Methods
    private void MoveHorizontal()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if(horizontalInput != 0)
        {
            if(horizontalInput > 0)
            {
                transform.localScale = new Vector3(2, 2, 1);
            }
            else if(horizontalInput < 0)
            {
                transform.localScale = new Vector3(-2, 2, 1);
            }
            interactionState = PlayerInteractionState.Walking;
            rb.velocity = new Vector2(horizontalInput * Horizontal_SPD, rb.velocity.y);
        }
        else
        {
            if(rb.velocity.x <= 0.01f && rb.velocity.x >= -0.01f)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                interactionState = PlayerInteractionState.Idle;
            }
        }
    }
    private void Jump()
    {
        if(Input.GetButtonDown("Jump") && (interactionState != PlayerInteractionState.Jumping || interactionState != PlayerInteractionState.Attacking) && physicalState == PlayerPhysicalState.Grounded)
        {
            rb.AddForce(new Vector2(0, Vertical_SPD), ForceMode2D.Impulse);
            interactionState = PlayerInteractionState.Jumping;
            physicalState = PlayerPhysicalState.Airborne;
        }
    }
    private void AnimatorUpdater()
    {
        // Update Animator Parameters based on physicalState and interactionState
    }

    // Action Methods
    private void AxeAttack()
    {
        //Collider2D[] enemy = Physics2D.OverlapBoxAll(ATKHitBoxInstance.transform.position, ATK_Size, enemies);
    }

    // Collision Methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            interactionState = PlayerInteractionState.Idle;
            physicalState = PlayerPhysicalState.Grounded;
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            HP -= 1;
            Debug.Log("Player HP: " + HP);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(ATKHitBoxInstance.transform.position, ATK_Size);
    }
}
