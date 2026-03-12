using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public Image characterImage;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;

    private Queue<DialogueLine> dialogueLines = new Queue<DialogueLine>();
    private Player playerRef;

    public bool isDialogueActive = false;
    public float textSpeed = 0.05f;
    public Animator animator;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        playerRef = FindObjectOfType<Player>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueActive = true;
        animator.SetBool("IsOpen", true);

        if(playerRef != null)
        {
            playerRef.SetMovementLocked(true);
        }

        dialogueLines.Clear();
        foreach (DialogueLine line in dialogue.lines)
        {
            dialogueLines.Enqueue(line);
        }

        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (dialogueLines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine line = dialogueLines.Dequeue();
        characterImage.sprite = line.character.image;
        characterName.text = line.character.name;
        StopAllCoroutines();
        StartCoroutine(TypeLine(line.line));
    }

    IEnumerator TypeLine(string line)
    {
        dialogueArea.text = "";
        foreach (char letter in line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        animator.SetBool("IsOpen", false);

        if(playerRef != null)
        {
            playerRef.SetMovementLocked(false);
        }
    }
}
