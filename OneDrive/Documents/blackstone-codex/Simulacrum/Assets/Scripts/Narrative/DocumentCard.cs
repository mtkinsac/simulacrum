using UnityEngine;
using UnityEngine.UI;

public class DocumentCard : MonoBehaviour
{
    [Header("Document Card Settings")]
    public Text titleText;
    public Text contentText;
    public Animator cardAnimator;

    private bool isOpen = false;

    public void ToggleCard()
    {
        isOpen = !isOpen;
        if (cardAnimator != null)
            cardAnimator.SetBool("IsOpen", isOpen);
        Debug.Log("DocumentCard " + (isOpen ? "opened." : "closed."));
    }

    public void SetContent(string title, string content)
    {
        if (titleText != null)
            titleText.text = title;
        if (contentText != null)
            contentText.text = content;
    }
}
