using UnityEngine;

public class GamingNexusController : MonoBehaviour
{
    public Transform nexusAnchor;

    void Start()
    {
        if (SimulacrumCoreManager.Instance != null)
        {
            SimulacrumCoreManager.Instance.RegisterModule("GamingNexus", gameObject);
        }
        else
        {
            Debug.LogWarning("GamingNexusController: SimulacrumCoreManager not found.");
        }
        Debug.Log("GamingNexusController: Initialized with anchor at " + nexusAnchor.position);
    }

    // Placeholder method for Nexus activation.
    public void ActivateNexus()
    {
        Debug.Log("GamingNexusController: Nexus activated.");
        // Future: Trigger scene transitions or narrative events.
    }
}
