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
    private float playerSpawnX, playerSpawnY;
    public float timer = 0.0f;
    public int hitsTaken = 0;

    //META OBJECTS
    public SceneManager SM;
    public CameraController CC;
    public PlayerHealthManager PHM;
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
    [SerializeField] private GameObject pauseScreen;

    void Start()
    {
        CC = GameObject.Find("Main_Camera").GetComponent<CameraController>();

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(debug == debugMode.OFF) { debug = debugMode.ON; }
            else { debug = debugMode.OFF; }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(volleyOrbPrefab, new Vector3(2, playerCharacter.transform.position.y, 0), Quaternion.identity);
        }

        if(state == gameState.PLAYING)
        {
            timer += Time.deltaTime;
        }
    }
}
