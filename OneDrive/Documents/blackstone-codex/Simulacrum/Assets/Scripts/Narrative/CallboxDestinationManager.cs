using UnityEngine;

public class CallboxDestinationManager : MonoBehaviour
{
    // Public method to set a new destination.
    public void SetDestination(string destinationId)
    {
        Debug.Log("CallboxDestinationManager: Destination set to " + destinationId);
        // Placeholder: Trigger transition events and integrate with ValeDialogueManager.
        EventManager.ValeSpeakRequest("Transitioning to destination: " + destinationId);
    }
}
