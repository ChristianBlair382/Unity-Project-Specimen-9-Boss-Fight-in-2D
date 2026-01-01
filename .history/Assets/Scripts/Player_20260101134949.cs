using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerState
{
    Idle,
    Walking,
    Jumping,
    Attacking
}

public class Player : MonoBehaviour
{
    // Player Statistics
    PlayerState currentState = PlayerState.Idle;
    private float Horizontal_SPD = 3f;
    private float Vertical_SPD = 5f;
    private int HP = 6;
    private int ATK_POW = 1;
    public Vector2 ATK_Size;

    // Unity Components
    private Rigidbody2D rb;
    public GameObject ATKHitBoxInstance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Handle Movement
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * Horizontal_SPD, rb.velocity.y);

        if(Input.GetButtonDown("Jump") && currentState != PlayerState.Jumping)
        {
            rb.AddForce(new Vector2(0, Vertical_SPD), ForceMode2D.Impulse);
            currentState = PlayerState.Jumping;
        }

        // Handle Attacking
        if(Input.GetMouseButtonDown(0) && currentState != PlayerState.Attacking)
        {
            currentState = PlayerState.Attacking;
        }
    }

    // Custom Methods
    private void AxeAttack()
    {
        Collider2D[] enemy = Physics2D.OverlapBoxAll(ATKHitBoxInstance.transform.position, meleeAttackSize, enemies);
    }

    // Handle Collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            currentState = PlayerState.Idle;
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            HP -= 1;
            Debug.Log("Player HP: " + HP);
        }
    }
}
