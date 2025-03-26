using UnityEngine;

public class VoicePromptController : MonoBehaviour
{
    [Header("Voice Prompt Settings")]
    public AudioSource promptAudio;
    public string promptText;

    public void ReceivePrompt(string command)
    {
        Debug.Log("VoicePromptController received command: " + command);
        // TODO: Integrate with offline GPT or dialog system.
        RespondToPrompt("Processing command: " + command);
    }

    public void RespondToPrompt(string response)
    {
        Debug.Log("VoicePromptController response: " + response);
        if (promptAudio != null)
            promptAudio.Play();
    }
}
