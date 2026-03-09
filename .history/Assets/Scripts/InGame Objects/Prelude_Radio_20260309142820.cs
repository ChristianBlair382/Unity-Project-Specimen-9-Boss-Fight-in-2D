using UnityEngine;

public class Prelude_Radio : Interaction_Box
{
    protected override void HandleInteraction()
    {
        SetPromptVisible(false);
        Debug.Log("Radio Dialogue Triggered");
    }
}
