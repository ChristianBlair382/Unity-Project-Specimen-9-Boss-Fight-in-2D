using UnityEngine;

public class Chest_Interaction : Interaction_Box
{
    protected override void HandleInteraction()
    {
        Debug.Log("Chest opened");
    }
}
