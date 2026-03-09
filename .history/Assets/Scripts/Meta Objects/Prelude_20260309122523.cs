using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prelude : MonoBehaviour
{
    //STATISTICS
    [SerializeField] private Transform playerSpawnTransform;

    //META OBJECTS
    public SceneController SC;
    public CameraController CC;

    //PREFABS AND INSTANCES
    public GameObject playerCharacterPrefab;
    public GameObject playerCharacter;

    private Player playerScript;
    // Start is called before the first frame update
    void Start()
    {
        SC = GameObject.Find("Scene_Controller").GetComponent<SceneController>();
        
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void SpawnPlayer()
    {
        playerCharacter = Instantiate(playerCharacterPrefab, playerSpawnTransform.position, Quaternion.identity);
        playerScript = playerCharacter.GetComponent<Player>();
        CC.target = playerCharacter.transform;
    }
}
