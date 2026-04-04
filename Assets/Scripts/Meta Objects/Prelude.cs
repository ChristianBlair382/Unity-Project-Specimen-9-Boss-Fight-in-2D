using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prelude : MonoBehaviour
{
    //STATISTICS
    private debugMode debug = debugMode.OFF;
    [SerializeField] private Transform playerSpawnTransform;

    //META OBJECTS
    public SceneController SC;
    public CameraController CC;

    //PREFABS AND INSTANCES
    public GameObject playerCharacterPrefab;
    public GameObject playerCharacter;
    public GameObject explosion_Small;
    public GameObject explosionPrefabInstance;

    private Player playerScript;
    // Start is called before the first frame update
    void Start()
    {
        SC = GameObject.Find("Scene_Controller").GetComponent<SceneController>();
        CC = GameObject.Find("Main_Camera").GetComponent<CameraController>();
        SpawnPlayer();
    }

    // Update is called once per frame
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
                Instantiate(explosionPrefabInstance, playerCharacter.transform.position, Quaternion.identity);
            }
        }
    }
    
    private void SpawnPlayer()
    {
        playerCharacter = Instantiate(playerCharacterPrefab, playerSpawnTransform.position, Quaternion.identity);
        playerScript = playerCharacter.GetComponent<Player>();
        CC.target = playerCharacter.transform;
    }
}
