using System;
using UnityEngine;

[Serializable]
public class MemoryObjectData
{
    public string memoryId;
    public string emotionTag;      // e.g., "somber", "joyful"
    public string intensityTag;    // e.g., "low", "medium", "high"
    public string realmTie;        // e.g., "WhisperingWoods"
    public string associatedCodex; // e.g., "CodexEntry_WhisperingWoods"
    public string reflectionLine;  // Reflection text for Vale's commentary
    // Additional fields (e.g., title, body) can be added as needed.
}
