using UnityEngine;

public static class ValeOfflineTemplateGenerator
{
    public static string GenerateFallbackPrompt(string memoryTag, string codexSummary)
    {
        return $"Vale, recalling the memory of {memoryTag} and reflecting on the codex summary: {codexSummary}. Share your wisdom.";
    }
}
