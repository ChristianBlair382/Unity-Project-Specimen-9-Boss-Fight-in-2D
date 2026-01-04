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
    [SerializeField] 
    private float 
        movementSpeed,
        lifetime;
    private GameObject player;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        //lifetime = 5.0f;
    }

    void Update()
    {
        lifetime -= Time.deltaTime;
        if(lifetime <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && currentState == orbState.Shot)
        {
            other.GetComponent<Player>().DamagePlayer(10);
        } else if (other.tag == "PlayerAttackHitBox" && currentState == orbState.Shot)
        {
            currentState = orbState.Reflected;
            rb.velocity = Vector2.zero;
            Vector2 direction = (other.transform.position - transform.position).normalized;
            rb.AddForce(-direction * movementSpeed, ForceMode2D.Impulse);
        } else if (other.tag == "Enemy" && currentState == orbState.Reflected)
        {
            other.GetComponent<Specimen_9>().Stun
            Destroy(gameObject);
        }
    }
}
