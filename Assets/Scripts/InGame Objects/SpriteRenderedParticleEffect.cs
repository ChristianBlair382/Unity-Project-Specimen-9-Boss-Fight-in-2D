using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRenderedParticleEffect : MonoBehaviour
{
    public float duration;
    public float rotation;
    public bool randomizeRotation;
    void Awake()
    {
        if(randomizeRotation)
        {
            rotation = Random.Range(0f, 360f);
        }
        transform.rotation = Quaternion.Euler(0f, 0f, rotation);
        Destroy(gameObject, duration);
    }
}
