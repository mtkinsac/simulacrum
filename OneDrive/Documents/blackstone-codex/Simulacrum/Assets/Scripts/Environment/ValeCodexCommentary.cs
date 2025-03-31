using UnityEngine;

// -------------------------
// 3. ValeCodexCommentary.cs
// -------------------------
public class ValeCodexCommentary : MonoBehaviour
{
    [Header("References")]
    public ValeDialogueManager valeDialogueManager;

    void OnEnable()
    {
        CodexRegistrationManager.OnEntryRegistered += HandleNewCodexEntry;
    }

    void OnDisable()
    {
        CodexRegistrationManager.OnEntryRegistered -= HandleNewCodexEntry;
    }

    private void HandleNewCodexEntry(MemoryEchoData data)
    {
        if (valeDialogueManager == null || data == null || string.IsNullOrEmpty(data.id)) return;
        string line = $"Ah, a new codex entry for {data.id}. Let us see what mysteries unfold.";
        valeDialogueManager.HandleValeSpeakRequest(line);
    }
}
