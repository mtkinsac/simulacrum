using UnityEngine;

public class VirtualMonitor : MonoBehaviour, IMonitorInteractable
{
    public void CloseMonitor()
    {
        Destroy(gameObject);
    }

    public void ResizeMonitor(float scaleFactor)
    {
        Vector3 newScale = transform.localScale * scaleFactor;
        newScale.x = Mathf.Clamp(newScale.x, 0.5f, 2.0f);
        newScale.y = Mathf.Clamp(newScale.y, 0.5f, 2.0f);
        newScale.z = Mathf.Clamp(newScale.z, 0.5f, 2.0f);
        transform.localScale = newScale;
    }
}
