using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeTheDead : DisruptionEffect
{
    public float activeTimer;
    [SerializeField]
    private GameObject 
        teleportPoint,
        respawnPoint;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    void Update()
    {
        rb.velocity = new Vector2(0.0f, 0.0f);
        if(activeTimer > 0.0f)
        {
            spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.20f);
            if(gameObject.transform.position.x >= teleportPoint.transform.position.x && gameObject.transform.position.y <= teleportPoint.transform.position.y)
            {
                gameObject.transform.position = respawnPoint.transform.position;
            }
            if (activeTimer <= 0)
            {
                activeTimer = 0.0f;
                spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            }
            activeTimer -= Time.deltaTime;
        }
    }
}
