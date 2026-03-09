using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prelude_Radio : MonoBehaviour
{
    public bool interactTrigger = false;
    private Player playerScript;
    private bool playerInRange = false;

    void Update()
    {
        if(playerInRange && playerScript != null)
        {
            bool canShowPrompt = playerScript.canInteract;
            playerScript.SetInteractionPromptVisible(canShowPrompt);
            interactTrigger = canShowPrompt;
        }

        if(interactTrigger)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                //Trigger radio dialogue here
                if(playerScript != null)
                {
                    playerScript.SetInteractionPromptVisible(false);
                }
                Debug.Log("Radio Dialogue Triggered");
                interactTrigger = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerScript = collision.GetComponent<Player>();
            playerInRange = playerScript != null;
            if(playerScript == null)
            {
                return;
            }

            // Initial sync on enter; Update() handles state changes while inside trigger.
            bool canShowPrompt = playerScript.canInteract;
            playerScript.SetInteractionPromptVisible(canShowPrompt);
            interactTrigger = canShowPrompt;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(playerScript != null)
            {
                playerScript.SetInteractionPromptVisible(false);
            }

            interactTrigger = false;
            playerInRange = false;
            playerScript = null;
        }
    }
}
