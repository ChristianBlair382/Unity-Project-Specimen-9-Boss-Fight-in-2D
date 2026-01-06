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
    [SerializeField] private orbState currentState = orbState.Shot;
    private Rigidbody2D rb;
    public float 
        movementSpeed,
        lifetime,
        knockbackMultiplier = 1.5f;
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
            rb.AddForce(-direction * (movementSpeed * knockbackMultiplier), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().DamagePlayer(10);
            Debug.Log("Player hit by Volley Orb.");
        } else if (other.tag == "Specimen9" && currentState == orbState.Reflected)
        {
            other.GetComponent<Specimen_9>().Stun();
            Debug.Log("Specimen_9 hit by reflected Volley Orb and stunned.");
            Destroy(gameObject);
        }
    }
}
