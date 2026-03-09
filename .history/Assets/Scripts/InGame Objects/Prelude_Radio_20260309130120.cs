using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prelude_Radio : MonoBehaviour
{
    public bool interactTrigger = false;
    public GameObject interactPromptPrefab;
    private GameObject interactPromptInstance;
    private 
    void Start()
    {
        
    }

    void Update()
    {
        if(interactTrigger)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                //Trigger radio dialogue here
                Debug.Log("Radio Dialogue Triggered");
                Destroy(interactPromptInstance);
                interactTrigger = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            interactPromptInstance = Instantiate(interactPromptPrefab, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
            interactPromptInstance.transform.SetParent(transform);
            interactTrigger = true;
        }
    }
}
