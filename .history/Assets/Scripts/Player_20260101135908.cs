using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerPhysicalState
{
    Grounded,
    Airborne
}
enum PlayerInteractionState
{
    Idle,
    Walking,
    Jumping,
    Attacking
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
    public GameObject ATKHitBoxInstance;
    public LayerMask enemies;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Handle Movement
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * Horizontal_SPD, rb.velocity.y);

        if(Input.GetButtonDown("Jump") && interactionState != PlayerInteractionState.Jumping && physicalState == PlayerPhysicalState.Grounded)
        {
            rb.AddForce(new Vector2(0, Vertical_SPD), ForceMode2D.Impulse);
            interactionState = PlayerInteractionState.Jumping;
            physicalState = PlayerPhysicalState.Airborne;
        }

        // Handle Attacking
        if(Input.GetMouseButtonDown(0) && interactionState != PlayerInteractionState.Attacking && physicalState == PlayerPhysicalState.Grounded)
        {
            interactionState = PlayerInteractionState.Attacking;
        }

        AnimatorUpdater();
    }

    // Custom Methods
    private void MoveHorizontal()
    {
        if()
    }
    private void AxeAttack()
    {
        Collider2D[] enemy = Physics2D.OverlapBoxAll(ATKHitBoxInstance.transform.position, ATK_Size, enemies);
    }

    private void AnimatorUpdater()
    {
        // Update Animator Parameters based on currentState
    }

    // Handle Collisions
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
}
