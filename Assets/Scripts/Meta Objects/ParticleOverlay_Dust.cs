using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOverlay_Dust : DisruptionEffect
{
    public Rigidbody2D rb;
    public GameObject LeftBound;
    public GameObject RightBound;
    public float horizontalSpeed = 0.5f;
    
    void Update()
    {
        rb.velocity = new Vector2(horizontalSpeed, 0.0f);
        if(gameObject.transform.position.x > RightBound.transform.position.x)
        {
            gameObject.transform.position = new Vector3(LeftBound.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        }
    }
}
