using UnityEngine;

public class GandorSummonController : MonoBehaviour
{
    // Gandor placeholder prefab (e.g., a glowing sphere)
    public GameObject gandorPrefab;

    // Optional: Particle effect prefab for summon animation stub
    public GameObject summonParticleEffect;

    // Optional: Audio clip for Gandor summon sound
    public AudioClip summonSound;

    // Audio source for playing sound effects
    private AudioSource audioSource;

    // Track the currently summoned Gandor instance (if any)
    private GameObject currentGandor;

    void Awake()
    {
        // Subscribe to the wand gesture event
        WandGestureListener.OnGandorGestureDetected += OnGandorGestureDetected;

        // Ensure an AudioSource is attached
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        WandGestureListener.OnGandorGestureDetected -= OnGandorGestureDetected;
    }

    private void OnGandorGestureDetected()
    {
        Debug.Log("Gandor summoning triggered by gesture.");
        SummonGandor();
    }

    public void SummonGandor()
    {
        if (currentGandor != null)
        {
            Debug.Log("Gandor is already summoned.");
            return;
        }

        // Example: summon Gandor two units in front of this object
        Vector3 summonPosition = transform.position + transform.forward * 2.0f;
        currentGandor = Instantiate(gandorPrefab, summonPosition, Quaternion.identity);
        Debug.Log("Gandor summoned at position: " + summonPosition);

        // BONUS: Trigger particle FX and sound for summoning
        if (summonParticleEffect != null)
        {
            Instantiate(summonParticleEffect, summonPosition, Quaternion.identity);
        }
        if (summonSound != null)
        {
            audioSource.PlayOneShot(summonSound);
        }

        // BONUS: Route a voice line via the stub voice line system
        RouteGandorVoiceLine("Gandor has arrived.");
    }

    public void DismissGandor()
    {
        if (currentGandor != null)
        {
            Destroy(currentGandor);
            Debug.Log("Gandor dismissed.");
        }
    }

    // New method for direct UI or quest-triggered speech
    public void SpeakLine(string message)
    {
        Debug.Log("Gandor speaks: " + message);
        // TODO: Integrate TTS or dialogue UI feedback here.
    }

    // BONUS: Stub for the voice line routing system (to be replaced with TTS/GPT integration)
    private void RouteGandorVoiceLine(string message)
    {
        Debug.Log("Gandor voice line: " + message);
        // TODO: Implement integration with TTSController or future GPT callback.
    }
}
