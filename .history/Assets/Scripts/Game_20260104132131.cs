using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public enum gameState { PLAYING, PAUSED, OVER }

public class Game : MonoBehaviour
{
    //STATISTICS
    private gameState state;
    private float playerSpawnX, playerSpawnY;
    public float timer = 0.0f;
    public int hitsTaken = 0;

    //META OBJECTS
    public SceneManager SC;
    public CameraController CC;
    public PlayerHealthManager PHM;
    public TimerRenderer TR;
    public GameObject playerCharacterPrefab;
    public GameObject playerCharacter;
    private Player playerScript;
    private GameObject pauseScreen;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
