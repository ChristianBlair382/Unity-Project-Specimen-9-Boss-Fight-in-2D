using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum gameState { PLAYING, PAUSED, OVER }

public class Game : MonoBehaviour
{
    //STATISTICS
    private gameState state;
    private float playerSpawnX, playerSpawnY;
    public float timer = 0.0f;
    public int hitsTaken = 0;

    //META OBJECTS
    public SceneController sceneController;
    public CameraController cameraController;
    public PlayerHealthManager playerHealthManager;
    public GameObject playerPrefab;
    public GameObject player;
    private Player playerScript;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
