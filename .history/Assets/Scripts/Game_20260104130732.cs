using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum gameState { PLAYING, PAUSED, OVER }

public class Game : MonoBehaviour
{
    private gameState state;
    private float playerSpawnX, playerSpawnY;
    public float timer;
    public int score = 0;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
