using UnityEngine;

public class HardModeButton : Interaction_Box
{
    [SerializeField] private DialogueTrigger dialogueTrigger;
    protected override void HandleInteraction()
    {
        if(dialogueTrigger != null)
        {
            dialogueTrigger.TriggerDialogue();
        }
    }

    public void EnableHardMode()
    {
        PlayerPrefs.SetInt("HardModeEnabled", 1);
    }
}
