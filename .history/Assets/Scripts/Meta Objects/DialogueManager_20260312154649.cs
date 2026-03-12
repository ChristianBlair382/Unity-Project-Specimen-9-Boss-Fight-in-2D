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
    private Coroutine typingRoutine;
    private bool isTyping = false;
    private string currentLineText = "";

    public bool isDialogueActive = false;
    public float textSpeed = 0.05f;
    [SerializeField] private Dialogue_Controller dialogueController;
    [SerializeField] private RectTransform dialogueBoxClickArea;
    [SerializeField] private Canvas dialogueCanvas;

    private void Update()
    {
        if(!isDialogueActive)
        {
            return;
        }

        if(Input.GetMouseButtonDown(0) && IsClickInsideDialogueBox())
        {
            if(isTyping)
            {
                CompleteCurrentLineInstantly();
            }
            else
            {
                DisplayNextLine();
            }
        }
    }

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

        if(dialogueBoxClickArea == null && dialogueArea != null)
        {
            dialogueBoxClickArea = dialogueArea.GetComponentInParent<RectTransform>();
        }

        ResolvePlayerReference();
        ResetDialogueVisuals();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if(isDialogueActive)
        {
            return;
        }

        isDialogueActive = true;

        ResolvePlayerReference();
        ResetDialogueVisuals();

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

        if(line.character != null)
        {
            characterName.text = line.character.name;
            if(characterImage != null)
            {
                characterImage.sprite = line.character.image;
                characterImage.enabled = line.character.image != null;
            }
        }
        else
        {
            characterName.text = "";
            if(characterImage != null)
            {
                characterImage.sprite = null;
                characterImage.enabled = false;
            }
        }

        if(typingRoutine != null)
        {
            StopCoroutine(typingRoutine);
        }

        typingRoutine = StartCoroutine(TypeLine(line.line));
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        currentLineText = line;
        dialogueArea.text = "";
        foreach (char letter in line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
        typingRoutine = null;
    }

    private IEnumerator BeginDialogueAfterOpenAnimation()
    {
        float waitTime = dialogueController != null ? dialogueController.GetOpenDelay() : 0f;
        if(waitTime > 0f)
        {
            yield return new WaitForSeconds(waitTime);
        }

        dialogueStartRoutine = null;
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

        if(typingRoutine != null)
        {
            StopCoroutine(typingRoutine);
            typingRoutine = null;
        }

        isTyping = false;
        currentLineText = "";

        if(playerRef != null)
        {
            playerRef.SetMovementLocked(false);
        }

        ResetDialogueVisuals();
    }

    private void ResolvePlayerReference()
    {
        if(playerRef != null)
        {
            return;
        }

        playerRef = FindObjectOfType<Player>();
        if(playerRef == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if(playerObject != null)
            {
                playerRef = playerObject.GetComponent<Player>();
            }
        }
    }

    private void ResetDialogueVisuals()
    {
        if(characterImage != null)
        {
            characterImage.sprite = null;
            characterImage.enabled = false;
        }

        if(characterName != null)
        {
            characterName.text = "";
        }

        if(dialogueArea != null)
        {
            dialogueArea.text = "";
        }
    }

    private bool IsClickInsideDialogueBox()
    {
        if(dialogueBoxClickArea == null)
        {
            return false;
        }

        Camera cameraForUI = null;
        if(dialogueCanvas != null && dialogueCanvas.renderMode != RenderMode.ScreenSpaceOverlay)
        {
            cameraForUI = dialogueCanvas.worldCamera;
        }

        return RectTransformUtility.RectangleContainsScreenPoint(dialogueBoxClickArea, Input.mousePosition, cameraForUI);
    }

    private void CompleteCurrentLineInstantly()
    {
        if(typingRoutine != null)
        {
            StopCoroutine(typingRoutine);
            typingRoutine = null;
        }

        dialogueArea.text = currentLineText;
        isTyping = false;
    }
}
