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

    private Player playerRef;
    private Coroutine dialogueStartRoutine;
    private Coroutine typingRoutine;
    private Dialogue activeDialogue;
    private DialogueLine currentLine;
    private int nextLineIndex;
    private bool isTyping = false;
    private bool waitingForChoice = false;
    private bool waitingToShowChoices = false;
    private string currentLineText = "";
    private readonly List<GameObject> activeChoiceButtons = new List<GameObject>();

    public bool isDialogueActive = false;
    public float textSpeed = 0.05f;
    [SerializeField] private Dialogue_Controller dialogueController;
    [SerializeField] private RectTransform dialogueBoxClickArea;
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private Image advanceDialogueIndicator;
    [SerializeField] private RectTransform responseOptionsContainer;

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
            else if(waitingToShowChoices)
            {
                ShowResponseOptions(currentLine.choices);
            }
            else if(!waitingForChoice)
            {
                if(currentLine != null && currentLine.endsDialogue)
                {
                    EndDialogue();
                }
                else
                {
                    DisplayNextLine();
                }
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

        if(!IsDialogueValid(dialogue))
        {
            return;
        }

        isDialogueActive = true;
        activeDialogue = dialogue;
        nextLineIndex = 0;

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

        if(dialogueStartRoutine != null)
        {
            StopCoroutine(dialogueStartRoutine);
        }

        dialogueStartRoutine = StartCoroutine(BeginDialogueAfterOpenAnimation());
    }

    public void DisplayNextLine()
    {
        if(waitingForChoice)
        {
            return;
        }

        if(activeDialogue == null || nextLineIndex >= activeDialogue.lines.Count)
        {
            EndDialogue();
            return;
        }

        waitingForChoice = false;
        ClearResponseOptions();
        SetAdvanceIndicatorVisible(false);

        currentLine = activeDialogue.lines[nextLineIndex];
        nextLineIndex++;

        if(currentLine.character != null)
        {
            characterName.text = currentLine.character.name;
            if(characterImage != null)
            {
                characterImage.sprite = currentLine.character.image;
                characterImage.enabled = currentLine.character.image != null;
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

        typingRoutine = StartCoroutine(TypeLine(currentLine.line));
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
        FinishDisplayingLine();
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
        currentLine = null;
        activeDialogue = null;
        waitingForChoice = false;
        waitingToShowChoices = false;

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
        ClearResponseOptions();

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

        SetAdvanceIndicatorVisible(false);
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
        typingRoutine = null;
        FinishDisplayingLine();
    }

    private void FinishDisplayingLine()
    {
        if(currentLine != null && currentLine.choices != null && currentLine.choices.Count > 0)
        {
            waitingToShowChoices = true;
            SetAdvanceIndicatorVisible(true);
            return;
        }

        SetAdvanceIndicatorVisible(true);
    }

    private void ShowResponseOptions(List<DialogueChoice> choices)
    {
        SetAdvanceIndicatorVisible(false);
        dialogueArea.text = "";

        RectTransform optionsContainer = GetResponseOptionsContainer();
        if(optionsContainer == null)
        {
            Debug.LogError("Dialogue choices cannot be shown because no UI parent is available.", this);
            EndDialogue();
            return;
        }

        waitingToShowChoices = false;
        waitingForChoice = true;
        SetAdvanceIndicatorVisible(false);
        dialogueArea.text = "";
        optionsContainer.gameObject.SetActive(true);

        for(int i = 0; i < choices.Count; i++)
        {
            DialogueChoice choice = choices[i];
            GameObject choiceObject = CreateResponseButton(optionsContainer, choice, i);
            activeChoiceButtons.Add(choiceObject);
        }
    }

    private RectTransform GetResponseOptionsContainer()
    {
        if(responseOptionsContainer != null)
        {
            return responseOptionsContainer;
        }

        RectTransform parent = dialogueBoxClickArea;
        if(parent == null && dialogueArea != null)
        {
            parent = dialogueArea.rectTransform.parent as RectTransform;
        }

        if(parent == null && dialogueCanvas != null)
        {
            parent = dialogueCanvas.transform as RectTransform;
        }

        if(parent == null)
        {
            return null;
        }

        GameObject containerObject = new GameObject(
            "Dialogue Response Options",
            typeof(RectTransform),
            typeof(VerticalLayoutGroup));
        responseOptionsContainer = containerObject.GetComponent<RectTransform>();
        responseOptionsContainer.SetParent(parent, false);
        containerObject.layer = parent.gameObject.layer;
        responseOptionsContainer.anchorMin = new Vector2(0.5f, 0.5f);
        responseOptionsContainer.anchorMax = new Vector2(0.5f, 0.5f);
        responseOptionsContainer.pivot = new Vector2(0.5f, 0.5f);
        responseOptionsContainer.anchoredPosition = new Vector2(-70f, 0f);
        responseOptionsContainer.sizeDelta = new Vector2(900f, 260f);

        VerticalLayoutGroup layout = containerObject.GetComponent<VerticalLayoutGroup>();
        layout.padding = new RectOffset(8, 8, 8, 8);
        layout.spacing = 8f;
        layout.childAlignment = TextAnchor.MiddleCenter;
        layout.childControlWidth = true;
        layout.childControlHeight = true;
        layout.childForceExpandWidth = true;
        layout.childForceExpandHeight = false;
        containerObject.SetActive(false);

        return responseOptionsContainer;
    }

    private GameObject CreateResponseButton(RectTransform parent, DialogueChoice choice, int index)
    {
        GameObject buttonObject = new GameObject(
            "Dialogue Choice " + (index + 1),
            typeof(RectTransform),
            typeof(Image),
            typeof(Button),
            typeof(LayoutElement));
        buttonObject.transform.SetParent(parent, false);
        buttonObject.layer = parent.gameObject.layer;

        Image background = buttonObject.GetComponent<Image>();
        background.color = new Color(0.12f, 0.12f, 0.12f, 0.92f);

        Button button = buttonObject.GetComponent<Button>();
        button.targetGraphic = background;
        button.onClick.AddListener(() => SelectChoice(choice));

        LayoutElement layoutElement = buttonObject.GetComponent<LayoutElement>();
        layoutElement.preferredHeight = 52f;

        GameObject labelObject = new GameObject(
            "Label",
            typeof(RectTransform),
            typeof(TextMeshProUGUI));
        labelObject.transform.SetParent(buttonObject.transform, false);
        labelObject.layer = buttonObject.layer;

        RectTransform labelRect = labelObject.GetComponent<RectTransform>();
        labelRect.anchorMin = Vector2.zero;
        labelRect.anchorMax = Vector2.one;
        labelRect.offsetMin = new Vector2(12f, 4f);
        labelRect.offsetMax = new Vector2(-12f, -4f);

        TextMeshProUGUI label = labelObject.GetComponent<TextMeshProUGUI>();
        if(dialogueArea != null)
        {
            label.font = dialogueArea.font;
        }
        label.fontSize = 30f;
        label.enableAutoSizing = true;
        label.fontSizeMin = 18f;
        label.fontSizeMax = 30f;
        label.color = Color.white;
        label.alignment = TextAlignmentOptions.MidlineLeft;
        label.text = choice.response;
        label.raycastTarget = false;

        return buttonObject;
    }

    private void SelectChoice(DialogueChoice choice)
    {
        if(!waitingForChoice || choice == null)
        {
            return;
        }

        waitingForChoice = false;
        ClearResponseOptions();
        if(choice.onSelected != null)
        {
            choice.onSelected.Invoke();
        }

        if(choice.nextLineIndex == -1)
        {
            EndDialogue();
            return;
        }

        if(activeDialogue == null ||
           choice.nextLineIndex < 0 ||
           choice.nextLineIndex >= activeDialogue.lines.Count)
        {
            Debug.LogError("Dialogue choice points to an invalid line index: " + choice.nextLineIndex, this);
            EndDialogue();
            return;
        }

        nextLineIndex = choice.nextLineIndex;
        DisplayNextLine();
    }

    private void ClearResponseOptions()
    {
        for(int i = 0; i < activeChoiceButtons.Count; i++)
        {
            GameObject choiceButton = activeChoiceButtons[i];
            if(choiceButton != null)
            {
                Destroy(choiceButton);
            }
        }
        activeChoiceButtons.Clear();

        if(responseOptionsContainer != null)
        {
            responseOptionsContainer.gameObject.SetActive(false);
        }
    }

    private bool IsDialogueValid(Dialogue dialogue)
    {
        if(dialogue == null || dialogue.lines == null || dialogue.lines.Count == 0)
        {
            Debug.LogError("Cannot start dialogue because it has no lines.", this);
            return false;
        }

        for(int lineIndex = 0; lineIndex < dialogue.lines.Count; lineIndex++)
        {
            DialogueLine line = dialogue.lines[lineIndex];
            if(line == null)
            {
                Debug.LogError("Dialogue contains a null line at index " + lineIndex + ".", this);
                return false;
            }

            if(line.choices == null)
            {
                continue;
            }

            for(int choiceIndex = 0; choiceIndex < line.choices.Count; choiceIndex++)
            {
                DialogueChoice choice = line.choices[choiceIndex];
                if(choice == null ||
                   choice.nextLineIndex < -1 ||
                   choice.nextLineIndex >= dialogue.lines.Count)
                {
                    Debug.LogError(
                        "Dialogue choice at line " + lineIndex + " has an invalid target line index.",
                        this);
                    return false;
                }
            }
        }

        return true;
    }

    private void SetAdvanceIndicatorVisible(bool isVisible)
    {
        if(advanceDialogueIndicator != null)
        {
            advanceDialogueIndicator.enabled = isVisible;
        }
    }
}
