using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationController : MonoBehaviour
{
    public XRController leftController;
    public XRController rightController;
    public Transform teleportTarget; // For testing purposes

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            TeleportTo(teleportTarget.position);
    }

    void TeleportTo(Vector3 destination)
    {
        transform.position = destination;
        Debug.Log("Teleported to: " + destination);
    }
}
