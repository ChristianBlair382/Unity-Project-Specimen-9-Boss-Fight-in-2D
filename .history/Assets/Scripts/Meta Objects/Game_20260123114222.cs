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

    //META OBJECTS
    public SceneManager SM;
    public CameraController CC;
    public PlayerHealthManager PHM;
    public PlayerStaminaManager PSM;
    public TimerRenderer TR;

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
        PHM = GameObject.Find("PlayerHealthManager").GetComponent<PlayerHealthManager>();
        PSM = GameObject.Find("PlayerStaminaManager").GetComponent<PlayerStaminaManager>();

        SpawnPlayer();
        PHM.InitializeWithPlayer();
        PSM.InitializeWithPlayer();
        SpawnSpecimen9();
    }

    void Update()
    {
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

        if(debug == debugMode.ON)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Instantiate(volleyOrbPrefab, new Vector3(10, 0, -2), Quaternion.identity);
            }
            if(Input.GetKeyDown(KeyCode.W))
            {
                Instantiate(ceilingProjectilePrefab, new Vector3(playerCharacter.transform.position.x, 6, -2), Quaternion.identity);
            }
            if(Input.GetKeyDown(KeyCode.E))
            {
                Instantiate(minionPrefab, new Vector3(10, 0, -1), Quaternion.identity);
            }
            if(Input.GetKeyDown(KeyCode.R))
            {
                Instantiate(handWavePrefab, new Vector3(playerCharacter.transform.position.x, playerCharacter.transform.position.y - 2.1f, -1.0f), Quaternion.identity);
            }
            if(Input.GetKeyDown(KeyCode.T))
            {
                Instantiate(bodyPillarPrefab, new Vector3(playerCharacter.transform.position.x, 0, -2), Quaternion.identity);
            }
        }
        

        if(state == gameState.PLAYING)
        {
            timer += Time.deltaTime;
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
}
