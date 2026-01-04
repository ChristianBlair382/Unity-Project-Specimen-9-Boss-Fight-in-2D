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
    }

    void Update()
    {
        
    }

    private void 
}
