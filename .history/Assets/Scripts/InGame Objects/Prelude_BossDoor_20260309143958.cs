using UnityEngine;

public class Prelude_BossDoor : Interaction_Box
{
    private SceneController SC;
    protected override void HandleInteraction()
    {
        Debug.Log("Boss Door Interaction Triggered");
    }
}
