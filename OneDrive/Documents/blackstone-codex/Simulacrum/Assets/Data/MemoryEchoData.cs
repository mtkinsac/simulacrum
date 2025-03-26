using UnityEngine;

[CreateAssetMenu(fileName = "MemoryEchoData", menuName = "Simulacrum/Memory Echo Data", order = 1)]
public class MemoryEchoData : ScriptableObject
{
    public string id;
    public string title;
    public string[] voiceLines;
    public AudioClip overrideAudio;
    public bool unlocksCodexEntry;
    public string codexCategory;
    public Sprite memoryIcon;
    public TextAsset additionalText;
    public bool requiresApproval; // For manual approval in codex.
    public bool isPersistentAcrossRealms; // Determines if hologram persists.
}
