using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prelude_Radio : MonoBehaviour
{
    private float interactionRadius = 2.0f;
    public GameObject interactPromptPrefab;
    private GameObject interactPromptInstance;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            interactPromptInstance = Instantiate(interactPromptPrefab, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
            interactPromptInstance.transform.SetParent(transform);
        }
    }
}
