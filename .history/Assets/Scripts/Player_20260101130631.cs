using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerState
{
    Idle,
    Running,
    Jumping
}

public class Player : MonoBehaviour
{
    PlayerState currentState = PlayerState.Idle;
    private float speed = 3f;
    private Rigidbody2D rb;

    private void Awake()
    {
        
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);

        if(Input.GetButtonDown("Jump") && )
        {
            rb.AddForce(new Vector2(0, 5f), ForceMode2D.Impulse);
        }
    }
}
