using UnityEngine;

public class DialogueContextBuilder : MonoBehaviour
{
    public static string BuildContext(string userPrompt)
    {
        SessionMemoryManager sessionManager = FindObjectOfType<SessionMemoryManager>();
        string memoryState = "No unlocked memories.";
        if (sessionManager != null && sessionManager.currentSessionState.unlockedHolograms.Count > 0)
        {
            memoryState = "Unlocked memories: " + string.Join(", ", sessionManager.currentSessionState.unlockedHolograms);
        }
        string codexHistory = "Codex entries: Dreamsmith Relics, Forged Legends.";
        string context = $"Vale, with memories [{memoryState}] and codex history [{codexHistory}], user prompt: {userPrompt}.";
        return context;
    }
}
