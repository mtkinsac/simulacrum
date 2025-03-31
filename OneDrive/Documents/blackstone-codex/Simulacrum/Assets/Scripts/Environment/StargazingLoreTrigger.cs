using UnityEngine;

public class StargazingLoreTrigger : MonoBehaviour
{
    [Header("Telescope Settings")]
    public Transform telescopeLens;
    public float detectionRange = 100f;

    [Header("Constellation Setup")]
    public string[] constellationIDs; 
    
    [Header("References")]
    public CodexRegistrationManager codexManager;
    public AudioSource stargazeAudio;
    public ParticleSystem shimmerFX;

    private bool hasTriggered = false;

    void Update()
    {
        if (CheckTelescopeUse() && !hasTriggered)
        {
            hasTriggered = true;
            TriggerStargazingLore();
        }
    }

    private bool CheckTelescopeUse()
    {
        return Input.GetKeyDown(KeyCode.T);
    }

    private void TriggerStargazingLore()
    {
        if (codexManager != null)
        {
            foreach (var constellation in constellationIDs)
            {
                Debug.Log($"StargazingLoreTrigger: Detected constellation '{constellation}'.");
            }
        }
        else
        {
            Debug.LogWarning("StargazingLoreTrigger: CodexRegistrationManager is not assigned.");
        }

        if (stargazeAudio != null) stargazeAudio.Play();
        if (shimmerFX != null) shimmerFX.Play();
    }
}
