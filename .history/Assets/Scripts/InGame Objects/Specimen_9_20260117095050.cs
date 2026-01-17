using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum InteractionState
{
    Waiting = 0,
    Attacking = 1,
    Stunned = 2
}

enum WellnessState
{
    Vulnerable = 0,
    Invulnerable = 1,
    Dead = 2
}

public class Specimen_9 : MonoBehaviour
{
    private InteractionState currentState = InteractionState.Waiting;
    private WellnessState currentWellness = WellnessState.Invulnerable;
    private int 
        HP = 60,
        numOfIFlashes = 10;
    [SerializeField] private float 
        stateTimer = 0f,
        stunTimer = 0f,
        waitDuration = 3f,
        stunDuration = 8f,
        transitionDuration = 1f,
        leftBound = -10f,
        rightBound = 10f;
    // Unity Components
    private Animator animator;
    private Rigidbody rb;
    private BoxCollider2D bc;
    private SpriteRenderer spriteRend;
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
        if (currentState == InteractionState.Stunned)
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0)
            {
                StartCoroutine(RecoveryTransition(transitionDuration));
                bc.size = new Vector2(.35f, 1f);
                bc.offset = new Vector2(-0.01f, .029f);
                currentState = InteractionState.Waiting;
            }
        } else
        {
            PerformAttackSequence();
        }
        FlipTowardsPlayer();
        animator.SetFloat("stun_timer", stunTimer);
    }
    private void FlipTowardsPlayer()
    {
        if(player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-2, 2, 1);
        } else
        {
            transform.localScale = new Vector3(2, 2, 1);
        }
    }
    private IEnumerator AttackCycle()
    {
        // The attack cycle will follow this sequence of attacks
        for(int i = 0; i < Random.Range(1, 4); i++)
        {
            yield return StartCoroutine(PerformAttack(1));
            yield return new WaitForSeconds(5.0f);
        }
        yield return StartCoroutine(PerformAttack(2));
        yield return new WaitForSeconds(8.0f);
        yield return StartCoroutine(PerformAttack(3));
        yield return new WaitForSeconds(5.0f);
        // Wait until all minions are dead before resuming attack cycle
        while(GameObject.FindGameObjectsWithTag("Minion").Length > 0)
        {
            if(HP <= 30) //If HP is below half, perform hand wave attacks while waiting
            {
                yield return StartCoroutine(PerformAttack(4));
                yield return new WaitForSeconds(1.0f);
            }
            yield return null;
        }
        yield return new WaitForSeconds(3.0f);
        yield return StartCoroutine(PerformAttack(0));
        // After completing the attack sequence, return to waiting state
        currentState = InteractionState.Waiting;
    }
    private void PerformAttackSequence()
    {
        // Define a sequence of attacks to perform during the attack phase
        // This can be customized as needed
        stateTimer -= Time.deltaTime;
        if (stateTimer <= 0)
        {
            if (currentState == InteractionState.Waiting)
            {
                currentState = InteractionState.Attacking;
                stateTimer = 3f; // Duration of attack phase
                //When attack phase starts, perform a series of attacks in a specific pattern
                StartCoroutine(AttackCycle());
            }
            else if (currentState == InteractionState.Attacking)
            {
                currentState = InteractionState.Waiting;
                stateTimer = waitDuration;
            }
        }
    }

    private IEnumerator PerformAttack(int attackType)
    {
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
                yield return StartCoroutine(SummonMinionsAttack(3));
                break;
            case 4:
                yield return StartCoroutine(HandWaveAttack());
                break;
        }
    }
    // Attack Types
    private IEnumerator VolleyAttack()
    {
        //Create a series of volley orbs that move towards the player
        //During windup, instantiate particle effects to indicate attack
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
        //Create body pillars at the player's current position
        for(int i = 0; i < 3; i++)
        {
            Instantiate(bodyPillarPrefab, new Vector3(player.transform.position.x, 0f, -2f), Quaternion.identity);
            yield return new WaitForSeconds(1.0f);
        }
    }
    private IEnumerator SummonMinionsAttack(int numberOfMinions)
    {
        //Create x minions at Specimen 9's position
        for(int i = 0; i < numberOfMinions; i++)
        {
            Instantiate(minionPrefab, new Vector3(transform.position.x, 0f, -1f), Quaternion.identity);
            yield return new WaitForSeconds(1.0f);
        }
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
        currentState = InteractionState.Stunned;
        bc.size = new Vector2(1, 0.692f);
        bc.offset = new Vector2(0f, 0f);
        animator.SetTrigger("stunned");
        StartCoroutine(StunTransition(transitionDuration));
    }
    private IEnumerator StunTransition(float duration)
    {
        //Upon being stunned, Specimen 9 will lerp to the ground and become susceptible to damage
        float elapsedTime = 0f;
        Vector3 startingPos = transform.position;
        Vector3 targetPos = new Vector3(transform.position.x, 0.99f, transform.position.z);
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startingPos, targetPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;
    }
    private IEnumerator LerpToPosition(Vector3 targetPos, float duration)
    {
        //Lerp from current position to target position over the specified duration
        float elapsedTime = 0f;
        Vector3 startingPos = transform.position;
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startingPos, targetPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;
    }

    private IEnumerator RecoveryTransition(float duration)
    {
        //When the stun timer ends, Specimen 9 will lerp back to its original position
        yield return StartCoroutine(LerpToPosition(new Vector3(transform.position.x, 1.5f, transform.position.z), duration));
        //Pause briefly at the top before lerping to a random horizontal position
        yield return new WaitForSeconds(0.75f);
        // After lerping back up, lerp to a random horizontal position within bounds
        float randomX = Random.Range(leftBound, rightBound);
        yield return StartCoroutine(LerpToPosition(new Vector3(randomX, 1.5f, transform.position.z), duration));
    }

    // Other Methods

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && currentState != InteractionState.Stunned && currentWellness != WellnessState.Dead)
        {
            collision.GetComponent<Player>().DamagePlayer(20);
        }
    }

    public void DamageEnemy(int DMG) 
    {
        if(currentWellness == WellnessState.Vulnerable)
        {
            HP -= DMG;
            Debug.Log("Specimen 9 took " + DMG + " damage. Current HP: " + HP);
            if (HP <= 0) 
            { 
                Invoke("Dead", 0.0f);
            }
            else 
            {
                StartCoroutine(DamageFlicker());
            }
        }
    }

    private IEnumerator DamageFlicker()
    {
        // Specimen 9 flickers when damaged
        currentWellness = WellnessState.Invulnerable;
        Physics2D.IgnoreLayerCollision(6, 7, true);
        for(int i = 0; i < numOfIFlashes; i++)
        {
            spriteRend.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            yield return new WaitForSeconds( 1 / numOfIFlashes );
            spriteRend.color = Color.white;
            yield return new WaitForSeconds( 1 / numOfIFlashes );
        }
        Physics2D.IgnoreLayerCollision(6, 7, false);
        currentWellness = WellnessState.Vulnerable;
    }
    
    private void Dead()
    {
        // Disable collider to prevent further interactions
        bc.enabled = false;
        // Play death animation coroutine
        StartCoroutine(DeathAnim());
    }

    private IEnumerator DeathAnim()
    {
        // Specimen 9 death animation
        spriteRend.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
        Debug.Log("Specimen 9 Defeated!");
    }
}
