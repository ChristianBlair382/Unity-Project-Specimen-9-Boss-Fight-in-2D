using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleConvergence : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;

    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void LateUpdate()
    {
        
    }
}
