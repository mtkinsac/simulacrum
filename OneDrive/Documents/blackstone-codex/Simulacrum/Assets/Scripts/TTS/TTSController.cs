using UnityEngine;

/// <summary>
/// Stubbed TTSController. Logs text output instead of using real speech synthesis.
/// Replace with platform-specific TTS implementation later.
/// </summary>
public class TTSController : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("TTSController initialized (stub mode).");
    }

    public void PlayText(string text)
    {
        Debug.Log($"[TTS STUB] Would speak: {text}");
    }
}
