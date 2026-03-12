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
    private Coroutine dialogueStartRoutine;

    public bool isDialogueActive = false;
    public float textSpeed = 0.05f;
    [SerializeField] private Dialogue_Controller dialogueController;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        if(dialogueController == null)
        {
            dialogueController = GetComponent<Dialogue_Controller>();
        }

        playerRef = FindObjectOfType<Player>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if(isDialogueActive)
        {
            return;
        }

        isDialogueActive = true;

        if(dialogueController != null)
        {
            dialogueController.OpenDialogueBox();
        }

        if(playerRef != null)
        {
            playerRef.SetMovementLocked(true);
        }

        dialogueLines.Clear();
        foreach (DialogueLine line in dialogue.lines)
        {
            dialogueLines.Enqueue(line);
        }

        if(dialogueStartRoutine != null)
        {
            StopCoroutine(dialogueStartRoutine);
        }

        dialogueStartRoutine = StartCoroutine(BeginDialogueAfterOpenAnimation());
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

    private IEnumerator BeginDialogueAfterOpenAnimation()
    {
        float waitTime = dialogueController != null ? dialogueController.GetOpenDelay() : 0f;
        if(waitTime > 0f)
        {
            yield return new WaitForSeconds(waitTime);
        }

        DisplayNextLine();
    }

    void EndDialogue()
    {
        isDialogueActive = false;

        if(dialogueController != null)
        {
            dialogueController.CloseDialogueBox();
        }

        if(dialogueStartRoutine != null)
        {
            StopCoroutine(dialogueStartRoutine);
            dialogueStartRoutine = null;
        }

        if(playerRef != null)
        {
            playerRef.SetMovementLocked(false);
        }
    }
}
