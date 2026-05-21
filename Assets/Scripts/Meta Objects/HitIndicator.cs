using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitIndicator : DisruptionEffect
{
    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
}
