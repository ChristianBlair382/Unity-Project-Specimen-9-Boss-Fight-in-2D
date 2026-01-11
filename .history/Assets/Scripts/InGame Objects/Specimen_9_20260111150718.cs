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
    private float leftBound = -10f;
    private float rightBound = 10f;
    // Unity Components
    private Animator animator;
    private Rigidbody rb;
    private BoxCollider2D bc;
    [SerializeField] private GameObject 
        player,
        volleyOrbPrefab,
        minionPrefab,
        bodyPillarPrefab,
        handWavePrefab,
        ceilingProjectilePrefab;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
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
    private IEnumerator HandWaveAttack()
    {
        float currentPositionX = transform.position.x;
        int direction, i = 0;
        if(player.transform.position.x > currentPositionX)
        {
            direction = 1;
        } else
        {
            direction = -1;
        }
        while (currentPositionX > leftBound && currentPositionX < rightBound)
        {
            Instantiate(handWavePrefab, new Vector2(currentPositionX + (direction * (2*i)), -2.1f), Quaternion.identity);
            i += 1;
            yield return new WaitForSeconds(1.0f);
        }
    }
    public void Stun()
    {
        float stunTimer = stunDuration;
        currentState = Specimen9State.Stunned;
    }
}
