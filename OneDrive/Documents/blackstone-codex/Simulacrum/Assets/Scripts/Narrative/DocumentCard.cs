using TMPro;
using UnityEngine;

public class DocumentCard : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI bodyText;

    public void SetContent(string title, string body)
    {
        if (titleText != null) titleText.text = title;
        if (bodyText != null) bodyText.text = body;
    }
}