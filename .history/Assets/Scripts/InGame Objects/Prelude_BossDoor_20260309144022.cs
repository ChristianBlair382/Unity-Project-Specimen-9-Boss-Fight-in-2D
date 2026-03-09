using UnityEngine;

public class Prelude_BossDoor : Interaction_Box
{
    private SceneController SC;
    void Start()
    {
        SC = GameObject.Find("Scene_Controller").GetComponent<SceneController>();
    }
    protected override void HandleInteraction()
    {
        Debug.Log("Boss Door Interaction Triggered");
        SC.LoadNextScene();
    }
}
