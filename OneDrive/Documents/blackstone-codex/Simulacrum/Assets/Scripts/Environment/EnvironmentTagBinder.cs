using UnityEngine;

// -------------------------
// 2. EnvironmentTagBinder.cs
// -------------------------
public class EnvironmentTagBinder : MonoBehaviour
{
    [Header("Tag Binding")]
    public string[] watchCategories;
    public string[] watchThemes;

    [Header("References")]
    public ParticleSystem artifactGlowFX;
    public AudioSource arcaneWhisperAudio;

    void OnEnable()
    {
        CodexRegistrationManager.OnEntryRegistered += OnCodexEntryRegistered;
    }

    void OnDisable()
    {
        CodexRegistrationManager.OnEntryRegistered -= OnCodexEntryRegistered;
    }

    private void OnCodexEntryRegistered(MemoryEchoData data)
    {
        if (data == null || data.additionalText == null) return;
        string content = data.additionalText.text;

        foreach (string cat in watchCategories)
        {
            if (content.Contains($"[Category: {cat}]"))
            {
                Debug.Log($"EnvironmentTagBinder: Found a newly registered {cat} entry. Triggering artifact glow.");
                artifactGlowFX?.Play();
            }
        }

        foreach (string theme in watchThemes)
        {
            if (content.Contains("[Themes:") && content.Contains(theme))
            {
                Debug.Log($"EnvironmentTagBinder: Found a newly registered entry with theme {theme}. Triggering arcane audio.");
                arcaneWhisperAudio?.Play();
            }
        }
    }
}
