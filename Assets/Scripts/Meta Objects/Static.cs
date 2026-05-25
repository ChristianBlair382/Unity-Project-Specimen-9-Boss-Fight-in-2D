using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Static : DisruptionEffect
{
    public float activeTimer;
    private static float rotation = 180.0f;
    private float rotationTimer = 0.0f;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    void Update()
    {
        if (activeTimer > 0.0f)
        {
            spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.20f);
            if(rotationTimer <= 0.0f)
            {
                rotationTimer = 0.5f;
                int randAxis = Random.Range(0, 2);
                switch (randAxis)
                {
                    case 0:
                        target.Rotate(rotation, 0.0f, 0.0f);
                        break;
                    case 1:
                        target.Rotate(0.0f, rotation, 0.0f);
                        break;
                    case 2:
                        target.Rotate(0.0f, 0.0f, rotation);
                        break;
                }
            }
            rotationTimer -= Time.deltaTime;
            activeTimer -= Time.deltaTime;
        } else
        {
            activeTimer = 0.0f;
            spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
    }
}
