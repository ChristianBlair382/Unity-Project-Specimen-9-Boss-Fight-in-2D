using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum orbState
{
    Shot = 0,
    Reflected = 1
}

public class VolleyOrb : MonoBehaviour
{
    private orbState currentState = orbState.Shot;
    private Rigidbody2D rb;
    public float 
        movementSpeed,
        lifetime;
    private GameObject player;
    private Transform playerPosition;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.transform;
        Vector2 direction = (playerPosition.position - transform.position).normalized;
        rb.AddForce(direction * movementSpeed, ForceMode2D.Impulse);
    }

    void Update()
    {
        lifetime -= Time.deltaTime;
        if(lifetime <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    public void ReflectOrb()
    {
        if (currentState == orbState.Shot)
        {
            currentState = orbState.Reflected;
            rb.velocity = Vector2.zero;
            Vector2 direction = (playerPosition.position - transform.position).normalized;
            rb.AddForce(-direction * movementSpeed, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && currentState == orbState.Shot)
        {
            other.GetComponent<Player>().DamagePlayer(10);
        } else if (other.tag == "Specimen" && currentState == orbState.Reflected)
        {
            other.GetComponent<Specimen_9>().Stun();
            Destroy(gameObject);
        }
    }
}
