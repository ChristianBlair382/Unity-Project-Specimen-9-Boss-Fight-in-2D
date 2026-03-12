using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public Image characterImage;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueText;

    private Queue<DialogueLine> dialogueLines;

    public bool isDialogueActive = false;
    public float textSpeed = 0.05f;
    public Animator animator;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueActive = true;
        animator.SetBool("IsOpen", true);
        LinkedList.
        dialogueLines = new Queue<DialogueLine>(dialogue.lines);
        DisplayNextLine();
    }
}
