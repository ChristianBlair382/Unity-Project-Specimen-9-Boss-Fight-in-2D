using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Specimen9State
{
    Waiting = 0,
    Attacking = 1,
    Stunned = 2
}

public class Specimen_9 : MonoBehaviour
{
    private Specimen9State currentState = Specimen9State.Waiting;
    private float stateTimer = 0f;
    private float attackDuration = 3f;
    private float stunDuration = 8f;
    // Unity Components
    private Animator animator;
    private Rigidbody rb;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void Stun()
    {
        //Add stun logic here
    }
}
