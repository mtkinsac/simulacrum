using UnityEngine;

public class PersistentHologram : MonoBehaviour
{
    [Header("Hologram Settings")]
    public string memoryId;  // Must match MemoryEchoData.id.
    public GameObject hologramVisual;  // The visual representation.

    void Start()
    {
        SessionMemoryManager sessionManager = FindObjectOfType<SessionMemoryManager>();
        if (sessionManager != null && sessionManager.currentSessionState.unlockedHolograms.Contains(memoryId))
        {
            if (hologramVisual != null)
                hologramVisual.SetActive(true);
            Debug.Log("PersistentHologram: Restored hologram for memory " + memoryId);
        }
        else
        {
            if (hologramVisual != null)
                hologramVisual.SetActive(false);
        }
    }

    public void UnlockHologram()
    {
        SessionMemoryManager sessionManager = FindObjectOfType<SessionMemoryManager>();
        if (sessionManager != null && !sessionManager.currentSessionState.unlockedHolograms.Contains(memoryId))
        {
            sessionManager.currentSessionState.unlockedHolograms.Add(memoryId);
            sessionManager.SaveSessionState();
            Debug.Log("PersistentHologram: Hologram unlocked and saved for memory " + memoryId);
        }
        if (hologramVisual != null)
            hologramVisual.SetActive(true);
    }
}
