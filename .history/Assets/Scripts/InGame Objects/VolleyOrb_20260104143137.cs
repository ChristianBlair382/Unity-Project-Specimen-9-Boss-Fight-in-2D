using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum orbState
{
    Idle = 0,
    Moving = 1
}

public class VolleyOrb : MonoBehaviour
{
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
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().DamagePlayer(10);
        } 
    }
}
