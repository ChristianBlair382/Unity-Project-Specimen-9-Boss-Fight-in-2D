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
    // Player Stat
    PlayerState currentState = PlayerState.Idle;
    private float speed = 3f;

    private Rigidbody2D rb;
    public GameObject ATKHitBoxInstance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);

        if(Input.GetButtonDown("Jump") && currentState != PlayerState.Jumping)
        {
            rb.AddForce(new Vector2(0, 5f), ForceMode2D.Impulse);
            currentState = PlayerState.Jumping;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            currentState = PlayerState.Idle;
        }
    }
}
