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
    public GameObject playerCharacterPrefab;
    public GameObject playerCharacter;
    private Player playerScript;
    [SerializeField] private GameObject pauseScreen;

    void Start()
    {
        CC = GameObject.Find("Main_Camera").GetComponent<CameraController>();

    }

    void Update()
    {
        if(Input.Get)
    }
}
