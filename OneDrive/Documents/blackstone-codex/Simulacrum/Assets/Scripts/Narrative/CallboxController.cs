using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CallboxController : MonoBehaviour
{
    public string selectedDestinationID;
    private DestinationLoader destinationLoader;
    private TransitionEffectsManager effectsManager;
    private RealmStateManager realmStateManager;

    void Start()
    {
        destinationLoader = FindObjectOfType<DestinationLoader>();
        effectsManager = FindObjectOfType<TransitionEffectsManager>();
        realmStateManager = FindObjectOfType<RealmStateManager>();

        if (destinationLoader == null)
            Debug.LogError("CallboxController: DestinationLoader not found.");

        if (effectsManager == null)
            Debug.LogError("CallboxController: TransitionEffectsManager not found.");

        if (realmStateManager == null)
            Debug.LogError("CallboxController: RealmStateManager not found.");
    }

    // Existing functionality: Activate callbox to initiate destination travel.
    public void ActivateCallbox()
    {
        Debug.Log("CallboxController: Callbox activated for destination: " + selectedDestinationID);

        if (effectsManager != null)
        {
            effectsManager.PlayFX("portal-flicker");
            StartCoroutine(DelayedLoad());
        }
        else
        {
            Debug.LogWarning("CallboxController: No transition effects available. Loading destination directly.");
            LoadDestinationAndUnlock();
        }
    }

    private IEnumerator DelayedLoad()
    {
        yield return new WaitForSeconds(2f); // Simulated transition time
        LoadDestinationAndUnlock();
    }

    private void LoadDestinationAndUnlock()
    {
        Debug.Log("CallboxController: Transition complete, loading destination...");
        destinationLoader.LoadDestination(selectedDestinationID);

        if (realmStateManager != null)
        {
            realmStateManager.UnlockDestination(selectedDestinationID);
        }
        else
        {
            Debug.LogWarning("CallboxController: RealmStateManager is null during UnlockDestination.");
        }
    }

    // --- Phase 3.0 Additions ---

    // Relay monitor summon commands to the SummonableMonitorController
    public void SummonMonitors(string type)
    {
        Debug.Log("CallboxController: Requesting summon of monitors type: " + type);
        if (SummonableMonitorController.Instance != null)
            SummonableMonitorController.Instance.SummonMonitor(type);
        else
            Debug.LogError("CallboxController: SummonableMonitorController instance not found.");
    }

    // Relay monitor dismissal command
    public void DismissMonitors()
    {
        Debug.Log("CallboxController: Requesting dismissal of all monitors.");
        if (SummonableMonitorController.Instance != null)
            SummonableMonitorController.Instance.DismissAllMonitors();
        else
            Debug.LogError("CallboxController: SummonableMonitorController instance not found.");
    }
}
