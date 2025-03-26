using UnityEngine;

public class ValeDialogueTriggerAPI : MonoBehaviour
{
    public static void QueueDialogue(string prompt)
    {
        if (ValeDialogueManager.Instance != null)
        {
            ValeDialogueManager.Instance.HandleValeSpeakRequest(prompt);
            Debug.Log("ValeDialogueTriggerAPI: Queued dialogue with prompt: " + prompt);
        }
        else
        {
            Debug.LogError("ValeDialogueTriggerAPI: ValeDialogueManager instance not found!");
        }
    }
}
