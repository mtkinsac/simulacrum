using UnityEngine;
using System.Collections;

public class ValeDialogueManager : MonoBehaviour
{
    public static ValeDialogueManager Instance;
    public TTSController ttsController;
    public ValeTransitionController valeTransition;
    public ChatGPTConnector chatConnector;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        EventManager.OnValeSpeakRequest += HandleValeSpeakRequest;
    }

    void OnDestroy()
    {
        EventManager.OnValeSpeakRequest -= HandleValeSpeakRequest;
    }

    public void HandleValeSpeakRequest(string prompt)
    {
        string context = AdaptiveDialogueContextBuilder.BuildContext(prompt);
        Debug.Log("ValeDialogueManager: Context built: " + context);
        StartCoroutine(ProcessDialogue(context));
    }

    IEnumerator ProcessDialogue(string context)
    {
        yield return StartCoroutine(chatConnector.SendDialogueRequest(context, OnDialogueResponse));
    }

    void OnDialogueResponse(string response)
    {
        Debug.Log("ValeDialogueManager: Received response: " + response);

        if (ttsController != null)
            ttsController.PlayText(response);
        else
            Debug.LogWarning("ValeDialogueManager: TTSController not assigned!");

        if (valeTransition != null)
            valeTransition.FadeIn();
        else
            Debug.LogWarning("ValeDialogueManager: ValeTransitionController not assigned!");
    }
}
