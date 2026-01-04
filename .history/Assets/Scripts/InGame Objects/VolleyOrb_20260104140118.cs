using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        lifetime = 5.0f;
    }

    void Update()
    {
        lifetime -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if(!other.GetComponent<Player>().isInvincible){
                other.GetComponent<Player>().DamagePlayer(1);
            } 
            Destroy(gameObject);
        } 
    }
}
