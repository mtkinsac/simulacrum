using UnityEngine;

public class MemoryMoodManager : MonoBehaviour
{
    public static MemoryMoodManager Instance;

    [Header("Global Emotional Levels (0–100)")]
    public float sadness = 0f;
    public float joy = 0f;
    public float awe = 0f;
    public float curiosity = 0f;

    [Header("Memory Resonance Index (0–100)")]
    public float memoryResonanceIndex = 0f;
    public float resonanceScale = 5f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Uncomment the following line to persist between scenes:
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Adjusts mood levels based on a given emotion and intensity.
    /// </summary>
    public void ApplyMemoryMood(string emotion, string intensity)
    {
        float intensityValue = GetIntensityValue(intensity);

        switch (emotion.ToLower())
        {
            case "somber":
            case "mournful":
                sadness += intensityValue;
                break;
            case "joyful":
            case "triumphant":
                joy += intensityValue;
                break;
            case "curious":
                curiosity += intensityValue;
                break;
            case "awe":
                awe += intensityValue;
                break;
            default:
                Debug.LogWarning($"MemoryMoodManager: Unrecognized emotion '{emotion}'.");
                break;
        }

        // Clamp all mood axes between 0 and 100
        sadness = Mathf.Clamp(sadness, 0f, 100f);
        joy = Mathf.Clamp(joy, 0f, 100f);
        awe = Mathf.Clamp(awe, 0f, 100f);
        curiosity = Mathf.Clamp(curiosity, 0f, 100f);

        // Update memory resonance index
        memoryResonanceIndex += intensityValue * resonanceScale;
        memoryResonanceIndex = Mathf.Clamp(memoryResonanceIndex, 0f, 100f);

        Debug.Log($"[Mood] {emotion} ({intensity}) → sadness:{sadness} joy:{joy} awe:{awe} curiosity:{curiosity} res:{memoryResonanceIndex}");
    }

    /// <summary>
    /// Returns a float value based on intensity ("low", "medium", or "high").
    /// </summary>
    private float GetIntensityValue(string intensity)
    {
        switch (intensity.ToLower())
        {
            case "low": return 5f;
            case "medium": return 10f;
            case "high": return 20f;
            default:
                Debug.LogWarning($"MemoryMoodManager: Unrecognized intensity '{intensity}', defaulting to Low.");
                return 5f;
        }
    }

    /// <summary>
    /// Returns a 4D vector summarizing the current mood (sadness, joy, awe, curiosity).
    /// </summary>
    public Vector4 GetMoodVector()
    {
        return new Vector4(sadness, joy, awe, curiosity);
    }

    /// <summary>
    /// Returns the overall memory resonance level (0–100).
    /// </summary>
    public float GetResonanceLevel()
    {
        return memoryResonanceIndex;
    }
}
