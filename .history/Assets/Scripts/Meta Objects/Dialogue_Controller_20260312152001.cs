using UnityEngine;

public class Dialogue_Controller : MonoBehaviour
{
    [SerializeField] private Animator dialogueAnimator;
    [SerializeField] private string openParameterName = "IsOpen";
    [SerializeField] private float openDelay = 0.15f;

    private void Awake()
    {
        if(dialogueAnimator == null)
        {
            dialogueAnimator = GetComponent<Animator>();
        }
    }

    public void OpenDialogueBox()
    {
        if(dialogueAnimator != null)
        {
            dialogueAnimator.SetBool(openParameterName, true);
        }
    }

    public void CloseDialogueBox()
    {
        if(dialogueAnimator != null)
        {
            dialogueAnimator.SetBool(openParameterName, false);
        }
    }

    public float GetOpenDelay()
    {
        return openDelay;
    }
}
