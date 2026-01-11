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
    [SerializeField] private float 
        stateTimer = 0f,
        waitDuration = 3f,
        stunDuration = 8f, 
        stunTimer = 0f,
        transitionDuration = 1f,
        leftBound = -10f,
        rightBound = 10f;
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
        stateTimer = waitDuration;
    }

    void Update()
    {
        if (currentState == Specimen9State.Stunned)
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0)
            {
                StartCoroutine(RecoveryTransition(transitionDuration));
                currentState = Specimen9State.Waiting;
            }
        } else
        {
            stateTimer -= Time.deltaTime;
            if (stateTimer <= 0)
            {
                if (currentState == Specimen9State.Waiting)
                {
                    currentState = Specimen9State.Attacking;
                    stateTimer = 3f; // Duration of attack phase
                    //When attack phase starts, perform a series of attacks in a specific pattern
                    StartCoroutine(PerformAttack());
                }
                else if (currentState == Specimen9State.Attacking)
                {
                    currentState = Specimen9State.Waiting;
                    stateTimer = waitDuration;
                }
            }
        }
        animator.SetFloat("stun_timer", (int)currentState);
    }

    private IEnumerator PerformAttack()
    {

        int attackType = Random.Range(0, 5);
        switch (attackType)
        {
            case 0:
                yield return StartCoroutine(VolleyAttack());
                break;
            case 1:
                yield return StartCoroutine(CeilingDropAttack());
                break;
            case 2:
                yield return StartCoroutine(BodyPillarAttack());
                break;
            case 3:
                yield return StartCoroutine(SummonMinionsAttack());
                break;
            case 4:
                yield return StartCoroutine(HandWaveAttack());
                break;
        }
    }
    // Attack Types
    private IEnumerator VolleyAttack()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 3; i++)
        {
            Instantiate(volleyOrbPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }
    private IEnumerator CeilingDropAttack()
    {
        //Create ceiling projectiles that drop down towards the player
        //Will spawn within a random range above the player
        float projectileSpawnX;
        int numberOfProjectiles = Random.Range(5, 10);
        if(player.transform.position.x < leftBound + 3)
        {
            //If player is too close to the edge, limit projectile spawn position range
            projectileSpawnX = Random.Range(player.transform.position.x, player.transform.position.x + 3);
        }
        else if(player.transform.position.x > rightBound - 3)
        {
            projectileSpawnX = Random.Range(player.transform.position.x - 3, player.transform.position.x);
        }
        else
        {
            projectileSpawnX = Random.Range(player.transform.position.x - 3, player.transform.position.x + 3);
        }
        for (int i = 0; i < 3; i++)
        {
            Instantiate(ceilingProjectilePrefab, new Vector3(projectileSpawnX, 6, -2), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }
    private IEnumerator BodyPillarAttack()
    {
        //Add body pillar attack logic here
        yield return null;
    }
    private IEnumerator SummonMinionsAttack()
    {
        //Add summon minions attack logic here
        yield return null;
    }
    private IEnumerator HandWaveAttack()
    {
        //Create hand waves moving towards the player from Specimen 9's position
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
            Instantiate(handWavePrefab, new Vector3(currentPositionX + (direction * (2*i)), -2.1f, -1f), Quaternion.identity);
            i += 1;
            yield return new WaitForSeconds(1.0f);
        }
    }
    public void Stun()
    {
        // Called when Specimen 9 is hit by a reflected volley orb
        stunTimer = stunDuration;
        currentState = Specimen9State.Stunned;
        animator.SetTrigger("stunned");
        StartCoroutine(StunTransition(transitionDuration));
    }
    private IEnumerator StunTransition(float duration)
    {
        //Upon being stunned, Specimen 9 will lerp to the ground and become susceptible to damage
        float elapsedTime = 0f;
        Vector3 startingPos = transform.position;
        Vector3 targetPos = new Vector3(transform.position.x, -1.5f, transform.position.z);
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startingPos, targetPos, elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;
    }
    private IEnumerator RecoveryTransition(float duration)
    {
        //When the stun timer ends, Specimen 9 will lerp back to its original position
        float elapsedTime = 0f;
        Vector3 startingPos = transform.position;
        Vector3 targetPos = new Vector3(transform.position.x, 0f, transform.position.z);
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startingPos, targetPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;
    }
}
