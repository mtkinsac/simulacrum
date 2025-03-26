using UnityEngine;
using TMPro;

public class TextBubbleUI : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public Animator animator;

    public void DisplayDialogue(string text)
    {
        if (dialogueText != null)
            dialogueText.text = text;
        if (animator != null)
            animator.SetTrigger("Show");
        Debug.Log("TextBubbleUI: Displaying dialogue.");
    }

    public void HideDialogue()
    {
        if (animator != null)
            animator.SetTrigger("Hide");
    }
}
