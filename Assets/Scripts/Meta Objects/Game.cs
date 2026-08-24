using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum debugMode { ON, OFF }

public enum gameState { PLAYING, PAUSED, OVER }

public class Game : MonoBehaviour
{
    //STATISTICS
    private debugMode debug = debugMode.OFF;
    private gameState state;
    [SerializeField] private Transform playerSpawnTransform;
    [SerializeField] private Transform specimen9SpawnTransform;
    public float timer = 0.0f;
    public int hitsTaken = 0;
    private float disruptionTimer = 10.0f;

    //META OBJECTS
    public SceneController SC;
    public CameraController CC;
    public PlayerHealthManager PHM;
    public PlayerStaminaManager PSM;
    public TimerRenderer TR;
    public VictoryTransition VictoryTransition;
    public Static staticEffect;
    public BloodDrip bloodDripEffect;
    public TakeTheDead takeTheDeadEffect;
    public HitIndicator hitIndicatorEffect;
    [SerializeField]
    public Canvas GUICanvas;
    public Canvas GameOverCanvas;

    //PREFABS AND INSTANCES
    public GameObject playerCharacterPrefab;
    public GameObject playerCharacter;
    public GameObject volleyOrbPrefab;
    public GameObject ceilingProjectilePrefab;
    public GameObject handWavePrefab;
    public GameObject bodyPillarPrefab;
    public GameObject minionPrefab;
    public GameObject specimen9Prefab;
    
    private Player playerScript;
    private Specimen_9 specimen9Script;
    //private GameObject pauseScreen;

    void Start()
    {
        CC = GameObject.Find("Main_Camera").GetComponent<CameraController>();
        PHM = GameObject.Find("PlayerHealthBar").GetComponent<PlayerHealthManager>();
        PSM = GameObject.Find("PlayerStaminaBar").GetComponent<PlayerStaminaManager>();

        SpawnPlayer();
        PHM.InitializeWithPlayer();
        PSM.InitializeWithPlayer();
        SpawnSpecimen9();
    }

    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(debug == debugMode.OFF) 
            { 
                debug = debugMode.ON;
                Debug.Log("Debug Mode ON");
            }
            else if (debug == debugMode.ON)
            { 
                debug = debugMode.OFF; 
                Debug.Log("Debug Mode OFF");
            }
        }
        */
        if(debug == debugMode.ON)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                //Instantiate(volleyOrbPrefab, new Vector3(10, 0, -2), Quaternion.identity);
                if(playerScript.infHealth)
                {
                    playerScript.infHealth = false;
                    Debug.Log("inf Health OFF");
                }
                else
                {
                    playerScript.infHealth = true;
                    Debug.Log("inf Health ON");
                }
            }
            if(Input.GetKeyDown(KeyCode.W))
            {
                //Instantiate(ceilingProjectilePrefab, new Vector3(playerCharacter.transform.position.x, 6, -2), Quaternion.identity);
                //Debug.Log("Blood Drip Disruption Activated");
                //bloodDripEffect.animator.SetTrigger("begin");
                Debug.Log("Specimen 9 HP Halved");
                specimen9Script.HP = specimen9Script.maxHP / 2;
            }
            if(Input.GetKeyDown(KeyCode.E))
            {
                //Instantiate(minionPrefab, new Vector3(10, 0, -1), Quaternion.identity);
                Debug.Log("Take The Dead Disruption Activated");
                takeTheDeadEffect.activeTimer = 5.0f;
            }
            if(Input.GetKeyDown(KeyCode.R))
            {
                //Instantiate(handWavePrefab, new Vector3(playerCharacter.transform.position.x, playerCharacter.transform.position.y - 2.1f, -1.0f), Quaternion.identity);
                Debug.Log("Victory Transition Activated");
                StartCoroutine(PlayVictoryTransition());
            }
            if(Input.GetKeyDown(KeyCode.T))
            {
                //Instantiate(bodyPillarPrefab, new Vector3(playerCharacter.transform.position.x, 0, -2), Quaternion.identity);
                Debug.Log("Hit Indicator Activated");
                hitIndicatorEffect.animator.SetTrigger("begin");
            }
        }
        
        if(state == gameState.PAUSED)
        {
            //PAUSE MENU LOGIC
        }

        if(specimen9Script.HP <= 0 || playerScript.GetHP() <= 0)
        {
            state = gameState.OVER;
        }

        if(state == gameState.PLAYING)
        {
            timer += Time.deltaTime;
            if(!specimen9Script.isStunned)
            {
                disruptionTimer -= Time.deltaTime;
            }
            if(disruptionTimer < 0)
            {
                int randomDisruption = Random.Range(0, 2);
                switch(randomDisruption)
                {
                    case 0:
                        //Debug.Log("Static Disruption Activated");
                        staticEffect.activeTimer = Random.Range(4.0f, 8.0f);
                        break;
                    case 1:
                        //Debug.Log("Blood Drip Disruption Activated");
                        bloodDripEffect.animator.SetTrigger("begin");
                        break;
                    case 2:
                        //Debug.Log("Take The Dead Disruption Activated");
                        takeTheDeadEffect.activeTimer = Random.Range(4.0f, 8.0f);
                        break;
                }

                if(specimen9Script.HP < specimen9Script.maxHP * 0.5f)
                {
                    disruptionTimer = Random.Range(4.0f, 10.0f); 
                } else
                {
                    disruptionTimer = Random.Range(8.0f, 17.0f); 
                }
            }
        }

        if(state == gameState.OVER)
        {
            if(playerScript.GetHP() <= 0)
            {
                
            } else
            {
                GUICanvas.enabled = false;
                playerScript.SetMovementLocked(true);
                StartCoroutine(PlayVictoryTransition());
            }
        }
    }

    private void SpawnPlayer()
    {
        playerCharacter = Instantiate(playerCharacterPrefab, playerSpawnTransform.position, Quaternion.identity);
        playerScript = playerCharacter.GetComponent<Player>();
        CC.target = playerCharacter.transform;
    }

    private void SpawnSpecimen9()
    {
        GameObject specimen9 = Instantiate(specimen9Prefab, specimen9SpawnTransform.position, Quaternion.identity);
        specimen9Script = specimen9.GetComponent<Specimen_9>();
    }

    private IEnumerator PlayVictoryTransition()
    {
        //Play transition animation
        yield return new WaitForSeconds(5.0f);
        VictoryTransition.animator.SetTrigger("begin");
    }
}
