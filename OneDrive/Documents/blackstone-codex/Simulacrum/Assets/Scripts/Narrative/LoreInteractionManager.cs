using UnityEngine;

public class LoreInteractionManager : MonoBehaviour
{
    public bool HasInteracted { get; private set; } = false;
    public TTSController ttsController;

    public delegate void LoreEventHandler(string memoryId);
    public static event LoreEventHandler OnLorePreviewed;
    public static event LoreEventHandler OnLoreHovered;

    public void TriggerMemory(string memoryID)
    {
        Debug.Log("LoreInteractionManager: Memory triggered with ID: " + memoryID);
        HasInteracted = true;
        EventManager.ProfileLoaded(); // Replace with OnLoreUnlocked event if available.

        SessionMemoryManager sessionManager = FindObjectOfType<SessionMemoryManager>();
        if (sessionManager != null)
        {
            Debug.Log("SessionMemoryManager: Logged memory interaction for: " + memoryID);
        }
        else
        {
            Debug.LogWarning("LoreInteractionManager: SessionMemoryManager not found!");
        }

        if (ttsController != null)
        {
            ttsController.PlayText("Memory unlocked: " + memoryID);
        }
        else
        {
            Debug.LogWarning("LoreInteractionManager: TTSController not assigned!");
        }
    }

    public void PreviewMemory(string memoryID)
    {
        OnLorePreviewed?.Invoke(memoryID);
        Debug.Log("LoreInteractionManager: Memory preview triggered for ID: " + memoryID);
    }
}
