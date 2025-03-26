using UnityEngine;

public class DeskAnchorController : MonoBehaviour
{
    public Transform deskCenter;

    void Start()
    {
        if (deskCenter != null)
        {
            transform.position = deskCenter.position;
            Debug.Log("Monitor anchored to desk center.");
        }
        else
        {
            Debug.LogWarning("Desk center not assigned to DeskAnchorController.");
        }
    }
}
