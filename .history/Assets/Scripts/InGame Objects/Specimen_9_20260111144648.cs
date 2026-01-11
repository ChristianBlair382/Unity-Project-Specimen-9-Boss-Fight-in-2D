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
    private float attackDuration = 5f;
    private float stunDuration = 8f;
    // Unity Components
    private Animator animator;
    private Rigidbody rb;
    private BoxCollider2D bc;
    private GameObject player;
    private GameObject volleyOrbPrefab;
    private GameObject minionPrefab;
    private GameObject bodyPillarPrefab;
    private GameObject handWavePrefab;
    private GameObject ceilingProjectilePrefab;
    

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider2D>();
        stateTimer = 5f;
    }

    void Update()
    {
        
    }
    // Attack Types
    private IEnumerator VolleyAttack()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(volleyOrbPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }
    private void CeilingDropAttack()
    {
        //Add ceiling drop attack logic here
    }
    private void BodyPillarAttack()
    {
        //Add body pillar attack logic here
    }
    private void SummonMinionsAttack()
    {
        //Add summon minions attack logic here
    }
    private void HandWaveAttack()
    {
        //Add hand wave attack logic here
    }
    public void Stun()
    {
        //Add stun logic here
    }
}
