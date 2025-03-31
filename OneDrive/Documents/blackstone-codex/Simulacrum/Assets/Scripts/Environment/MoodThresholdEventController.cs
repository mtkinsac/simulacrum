using UnityEngine;
using UnityEngine.Events;

public class MoodThresholdEventController : MonoBehaviour
{
    [Header("Threshold Settings")]
    public float aweThreshold = 80f;
    public UnityEvent onAweThresholdReached;

    private bool aweThresholdReached = false;

    void Update()
    {
        if (MemoryMoodManager.Instance == null) return;
        float currentAwe = MemoryMoodManager.Instance.awe;

        if (!aweThresholdReached && currentAwe >= aweThreshold)
        {
            aweThresholdReached = true;
            Debug.Log("MoodThresholdEventController: Awe threshold reached!");
            onAweThresholdReached?.Invoke();
        }
        else if (aweThresholdReached && currentAwe < aweThreshold - 5f)
        {
            aweThresholdReached = false;
        }
    }
}
