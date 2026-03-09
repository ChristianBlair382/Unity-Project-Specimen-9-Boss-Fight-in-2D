using UnityEngine;

public abstract class Interaction_Box : MonoBehaviour
{
    [SerializeField] private KeyCode interactionKey = KeyCode.E;
    [SerializeField] private bool disableAfterInteract = false;
    [SerializeField] private bool allowRepeatInteractionWhileInRange = false;

    protected Player playerScript;
    protected bool playerInRange = false;
    protected bool interactTrigger = false;
    private bool hasInteracted = false;
    private bool hasInteractedThisRange = false;

    protected virtual void Update()
    {
        SyncInteractionState();

        if(interactTrigger && Input.GetKeyDown(interactionKey))
        {
            HandleInteraction();

            if(!allowRepeatInteractionWhileInRange)
            {
                hasInteractedThisRange = true;
                SetPromptVisible(false);
                interactTrigger = false;
            }

            if(disableAfterInteract)
            {
                hasInteracted = true;
                SetPromptVisible(false);
                interactTrigger = false;
            }
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player") || hasInteracted)
        {
            return;
        }

        playerScript = collision.GetComponent<Player>();
        playerInRange = playerScript != null;
        hasInteractedThisRange = false;
        SyncInteractionState();
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
        {
            return;
        }

        SetPromptVisible(false);
        interactTrigger = false;
        playerInRange = false;
        hasInteractedThisRange = false;
        playerScript = null;
    }

    protected virtual void OnDisable()
    {
        SetPromptVisible(false);
        interactTrigger = false;
        playerInRange = false;
        hasInteractedThisRange = false;
        playerScript = null;
    }

    protected abstract void HandleInteraction();

    private void SyncInteractionState()
    {
        if(!playerInRange || playerScript == null || hasInteracted || hasInteractedThisRange)
        {
            SetPromptVisible(false);
            interactTrigger = false;
            return;
        }

        bool canShowPrompt = playerScript.canInteract;
        SetPromptVisible(canShowPrompt);
        interactTrigger = canShowPrompt;
    }

    protected void SetPromptVisible(bool isVisible)
    {
        if(playerScript != null)
        {
            playerScript.SetInteractionPromptVisible(isVisible);
        }
    }
}
