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
        int count = ps.particleCount;

        if (particles == null || particles.Length < count)
            particles = new ParticleSystem.Particle[count];

        ps.GetParticles(particles);

        for (int i = 0; i < count; i++)
        {
            Vector3 dir = (target.position - particles[i].position);
            particles[i].velocity = dir.normalized * speed;
        }

        ps.SetParticles(particles, count);
    }
}
