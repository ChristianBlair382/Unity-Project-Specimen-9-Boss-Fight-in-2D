using UnityEngine;

public class Prelude_Radio : Interaction_Box
{
    [SerializeField] private DialogueTrigger dialogueTrigger;

    protected override void HandleInteraction()
    {
        if(dialogueTrigger != null)
        {
            dialogueTrigger.TriggerDialogue();
        }
    }
}
