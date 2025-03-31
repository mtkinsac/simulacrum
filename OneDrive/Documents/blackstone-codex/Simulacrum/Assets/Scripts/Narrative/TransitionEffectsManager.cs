using UnityEngine;

// [Optional: Define AmbientFXProfile here or replace with your version if ScriptableObject]
[System.Serializable]
public class AmbientFXProfile
{
    public string fxName;
    public GameObject effectPrefab;
}

public class TransitionEffectsManager : MonoBehaviour
{
    public static TransitionEffectsManager Instance;

    public AmbientFXProfile[] ambientProfiles;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlayFX(string fxName)
    {
        foreach (var profile in ambientProfiles)
        {
            if (profile.fxName == fxName && profile.effectPrefab != null)
            {
                Instantiate(profile.effectPrefab, transform.position, Quaternion.identity);
                return;
            }
        }
        Debug.LogWarning("FX not found: " + fxName);
    }

    public void PlayTransition(string transitionName)
    {
        Debug.Log("TransitionEffectsManager: Playing transition effect: " + transitionName);
        PlayFX(transitionName);
    }
}
