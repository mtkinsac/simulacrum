using UnityEngine;
using System;

public class TransitionEffectsManager : MonoBehaviour
{
    private AmbientFXProfile[] fxProfiles;

    void Awake()
    {
        fxProfiles = Resources.LoadAll<AmbientFXProfile>("AmbientFXProfiles");
        Debug.Log("TransitionEffectsManager: Loaded " + fxProfiles.Length + " ambient FX profiles.");
    }

    public AmbientFXProfile GetProfileForDestination(string destinationID)
    {
        foreach (AmbientFXProfile profile in fxProfiles)
        {
            if (profile.destinationID.Equals(destinationID, StringComparison.OrdinalIgnoreCase))
            {
                return profile;
            }
        }
        Debug.LogWarning("TransitionEffectsManager: No ambient FX profile found for " + destinationID + ". Using fallback.");
        AmbientFXProfile fallback = Resources.Load<AmbientFXProfile>("AmbientFXProfile_Fallback");
        return fallback;
    }

    public void TriggerTransitionEffects(string destinationID)
    {
        AmbientFXProfile profile = GetProfileForDestination(destinationID);
        if (profile != null)
        {
            Debug.Log("TransitionEffectsManager: Triggering FX for destination: " + destinationID);
            // Example: Set ambient audio, adjust lighting, instantiate visual effects.
        }
    }
}
