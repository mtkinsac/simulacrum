using UnityEngine;
using System.Collections.Generic;
#if UNITY_STANDALONE_WIN
using System.Speech.Synthesis;
#endif

public class TTSController : MonoBehaviour
{
#if UNITY_STANDALONE_WIN
    private SpeechSynthesizer synthesizer;
#endif
    private Queue<string> ttsQueue = new Queue<string>();
    private bool isSpeaking = false;

    void Start()
    {
#if UNITY_STANDALONE_WIN
        try
        {
            synthesizer = new SpeechSynthesizer();
            synthesizer.SpeakCompleted += OnSpeakCompleted;
        }
        catch (System.Exception ex)
        {
            Debug.LogError("TTS initialization failed: " + ex.Message);
        }
#endif
    }

    public void PlayText(string content)
    {
        if (string.IsNullOrEmpty(content))
        {
            Debug.LogWarning("TTS content is empty");
            return;
        }
        ttsQueue.Enqueue(content);
        if (!isSpeaking)
            ProcessQueue();
    }

#if UNITY_STANDALONE_WIN
    private void ProcessQueue()
    {
        if (ttsQueue.Count > 0 && synthesizer != null)
        {
            isSpeaking = true;
            string nextContent = ttsQueue.Dequeue();
            try
            {
                synthesizer.SpeakAsync(nextContent);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("TTS SpeakAsync failed: " + ex.Message);
                isSpeaking = false;
            }
        }
        else
            isSpeaking = false;
    }

    private void OnSpeakCompleted(object sender, SpeakCompletedEventArgs e)
    {
        isSpeaking = false;
        ProcessQueue();
    }
#else
    private void ProcessQueue() { isSpeaking = false; }
#endif

    public void AssignVoice(string voiceName)
    {
#if UNITY_STANDALONE_WIN
        try
        {
            if (synthesizer != null)
                synthesizer.SelectVoice(voiceName);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to assign voice: " + ex.Message);
        }
#endif
    }

    public void SetPitchSpeed(float pitch, float speed)
    {
#if UNITY_STANDALONE_WIN
        try
        {
            if (synthesizer != null)
                synthesizer.Rate = (int)speed;
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to set pitch/speed: " + ex.Message);
        }
#endif
    }
}
