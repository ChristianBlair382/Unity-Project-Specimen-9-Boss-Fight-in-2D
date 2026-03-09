using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prelude_Radio : MonoBehaviour
{
    public bool interactTrigger = false;
    public GameObject interactPromptPrefab;
    private GameObject interactPromptInstance;
    private Player playerScript;

    void Update()
    {
        if(interactTrigger)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                //Trigger radio dialogue here
                Destroy(interactPromptInstance);
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
                    interactPromptInstance = Instantiate(interactPromptPrefab, collision.transform.position + new Vector3(0, 1.25f, -0.5f), Quaternion.identity);
                    interactPromptInstance.transform.SetParent(collision.transform);
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
                Destroy(interactPromptInstance);
                interactTrigger = false;
            }
        }
    }
}
