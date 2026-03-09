using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prelude_Radio : MonoBehaviour
{
    public bool interactTrigger = false;
    private Player playerScript;

    void Update()
    {
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
            if(playerScript == null)
            {
                return;
            }
            else
            {
                //Check if the player is grounded before allowing interaction with the radio
                if(playerScript.canInteract)
                {
                    playerScript.SetInteractionPromptVisible(true);
                    interactTrigger = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(interactTrigger)
            {
                if(playerScript != null)
                {
                    playerScript.SetInteractionPromptVisible(false);
                }
                interactTrigger = false;
            }

            playerScript = null;
        }
    }

    private void OnDisable()
    {
        if(playerScript != null)
        {
            playerScript.SetInteractionPromptVisible(false);
        }
    }
}
