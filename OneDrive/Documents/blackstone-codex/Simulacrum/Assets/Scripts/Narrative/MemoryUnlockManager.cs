using UnityEngine;
using System;

public class MemoryUnlockManager : MonoBehaviour
{
    // Event signaling that a memory has been unlocked (memoryId passed as parameter)
    public static event Action<string> OnMemoryUnlocked;

    /// <summary>
    /// Unlocks a memory based on its data.
    /// </summary>
    public void UnlockMemory(MemoryObjectData memoryData)
    {
        if (memoryData == null)
        {
            Debug.LogError("MemoryUnlockManager: memoryData is null. Cannot unlock memory.");
            return;
        }

        Debug.Log($"MemoryUnlockManager: Unlocking memory '{memoryData.memoryId}'...");

        // 1) Update mood using MemoryMoodManager.
        if (MemoryMoodManager.Instance != null)
        {
            MemoryMoodManager.Instance.ApplyMemoryMood(memoryData.emotionTag, memoryData.intensityTag);
        }
        else
        {
            Debug.LogWarning("MemoryUnlockManager: MemoryMoodManager instance not found.");
        }

        // 2) Trigger Vale's reflection if a reflection line exists.
        if (!string.IsNullOrEmpty(memoryData.reflectionLine))
        {
            ValeReflectionManager reflectionMgr = FindObjectOfType<ValeReflectionManager>();
            if (reflectionMgr != null)
            {
                reflectionMgr.TriggerMemoryReflection(memoryData);
            }
            else
            {
                Debug.LogWarning("MemoryUnlockManager: ValeReflectionManager not found.");
            }
        }

        // 3) Unlock associated codex entry, if specified.
        if (!string.IsNullOrEmpty(memoryData.associatedCodex))
        {
            Debug.Log($"MemoryUnlockManager: Unlocking associated codex entry '{memoryData.associatedCodex}'.");
            // Uncomment when CodexRegistrationManager is available:
            // CodexRegistrationManager.Instance.UnlockEntry(memoryData.associatedCodex);
        }

        // 4) Realm/environment-specific logic can be added here.
        if (!string.IsNullOrEmpty(memoryData.realmTie))
        {
            Debug.Log($"MemoryUnlockManager: Memory '{memoryData.memoryId}' is tied to realm '{memoryData.realmTie}'.");
            // Raise events or call additional managers if needed.
        }

        // 5) Broadcast that this memory has been unlocked.
        OnMemoryUnlocked?.Invoke(memoryData.memoryId);
    }
}
