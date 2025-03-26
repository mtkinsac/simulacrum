using UnityEngine;

public class MonitorResizer : MonoBehaviour
{
    public float minScale = 0.5f;
    public float maxScale = 2.0f;

    public void ResizeMonitor(float scaleFactor)
    {
        Vector3 newScale = transform.localScale * scaleFactor;
        newScale.x = Mathf.Clamp(newScale.x, minScale, maxScale);
        newScale.y = Mathf.Clamp(newScale.y, minScale, maxScale);
        newScale.z = Mathf.Clamp(newScale.z, minScale, maxScale);
        transform.localScale = newScale;
        Debug.Log("Monitor resized. New scale: " + transform.localScale);
    }
}
