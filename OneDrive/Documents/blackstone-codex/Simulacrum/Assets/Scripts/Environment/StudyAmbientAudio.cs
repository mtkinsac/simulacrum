using UnityEngine;

public class StudyAmbientAudio : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource bookWhisperAudio;
    public AudioSource fireplaceAudio;

    [Header("Proximity Settings")]
    public Transform player;
    public float whisperActivationRadius = 3f;
    public float fireplaceRadius = 2f;

    void Update()
    {
        if (player == null) return;

        float distToBooks = Vector3.Distance(player.position, transform.position);
        if (bookWhisperAudio != null)
        {
            float whisperVolume = Mathf.InverseLerp(whisperActivationRadius, 0f, distToBooks);
            bookWhisperAudio.volume = Mathf.Clamp01(whisperVolume);
        }

        if (fireplaceAudio != null)
        {
            float fireVolume = Mathf.InverseLerp(fireplaceRadius, 0f, distToBooks);
            fireplaceAudio.volume = Mathf.Clamp01(fireVolume) * 0.8f;
        }
    }
}
