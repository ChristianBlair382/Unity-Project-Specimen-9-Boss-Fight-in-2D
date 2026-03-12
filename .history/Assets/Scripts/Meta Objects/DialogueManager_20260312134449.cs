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
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
