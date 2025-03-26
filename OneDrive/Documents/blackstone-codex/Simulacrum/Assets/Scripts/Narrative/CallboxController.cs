using UnityEngine;
using UnityEngine.SceneManagement;

public class CallboxController : MonoBehaviour
{
    public string selectedDestinationID;
    private DestinationLoader destinationLoader;
    private TransitionEffectsManager effectsManager;

    void Start()
    {
        destinationLoader = FindObjectOfType<DestinationLoader>();
        effectsManager = FindObjectOfType<TransitionEffectsManager>();

        if (destinationLoader == null)
            Debug.LogError("CallboxController: DestinationLoader not found!");
        if (effectsManager == null)
            Debug.LogError("CallboxController: TransitionEffectsManager not found!");
    }

    // Called when a destination is selected via UI or TARDIS interaction.
    public void SelectDestination(string destinationID)
    {
        selectedDestinationID = destinationID;
        Debug.Log("CallboxController: Destination selected: " + destinationID);
    }

    // Called when the player confirms their destination choice.
    public void ConfirmDestination()
    {
        if (string.IsNullOrEmpty(selectedDestinationID))
        {
            Debug.LogWarning("CallboxController: No destination selected!");
            return;
        }

        DestinationData dest = destinationLoader.GetDestinationByID(selectedDestinationID);
        if (dest != null)
        {
            // Trigger ambient FX for the destination.
            effectsManager.TriggerTransitionEffects(selectedDestinationID);

            // Update realm state.
            RealmStateManager realmState = FindObjectOfType<RealmStateManager>();
            if (realmState != null)
            {
                realmState.SetLastVisitedRealm(selectedDestinationID);
                realmState.UnlockDestination(selectedDestinationID);
            }
            else
            {
                Debug.LogWarning("CallboxController: RealmStateManager not found!");
            }

            // Simulate scene transition after a delay.
            Invoke("LoadDestinationScene", 3f);
        }
        else
        {
            Debug.LogError("CallboxController: Destination data not found for ID: " + selectedDestinationID);
        }
    }

    void LoadDestinationScene()
    {
        DestinationData dest = destinationLoader.GetDestinationByID(selectedDestinationID);
        if (dest != null && !string.IsNullOrEmpty(dest.sceneName))
        {
            Debug.Log("CallboxController: Loading scene: " + dest.sceneName);
            SceneManager.LoadScene(dest.sceneName);
        }
        else
        {
            Debug.LogWarning("CallboxController: Scene name not specified; staying in current scene.");
        }
    }
}
