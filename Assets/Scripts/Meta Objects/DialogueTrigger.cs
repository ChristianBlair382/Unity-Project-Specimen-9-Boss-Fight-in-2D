using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueCharacter
{
    public string name;
    public Sprite image;
}

[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;
    [Tooltip("End the dialogue when the player advances past this line.")]
    public bool endsDialogue;
    public List<DialogueChoice> choices = new List<DialogueChoice>();
}

[System.Serializable]
public class DialogueChoice
{
    public string response;
    [Tooltip("Zero-based index of the line to show next. Use -1 to end the dialogue.")]
    public int nextLineIndex = -1;
    [Tooltip("Functions to invoke when this response is selected.")]
    public UnityEvent onSelected = new UnityEvent();
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> lines = new List<DialogueLine>();
}

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        if(!DialogueManager.instance.isDialogueActive)
        {
            DialogueManager.instance.StartDialogue(dialogue);
        }
    }
}
