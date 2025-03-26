using UnityEngine;
using System.Collections.Generic;

public class VirtualMonitorManager : MonoBehaviour
{
    [Header("Monitor Settings")]
    public GameObject virtualMonitorPrefab;
    public Transform spawnPoint;
    public int maxMonitors = 5;

    private Queue<GameObject> monitorPool = new Queue<GameObject>();

    void Start()
    {
        if (virtualMonitorPrefab == null || spawnPoint == null)
        {
            Debug.LogError("VirtualMonitorManager: Prefab or spawnPoint not assigned.");
            return;
        }

        for (int i = 0; i < maxMonitors; i++)
        {
            GameObject monitor = Instantiate(virtualMonitorPrefab, spawnPoint.position, spawnPoint.rotation);
            monitor.SetActive(false);
            monitorPool.Enqueue(monitor);
        }
    }

    void Update()
    {
        if (CheckHandGesture() || Input.GetKeyDown(KeyCode.M))
            SpawnOrRecycleMonitor();
    }

    bool CheckHandGesture()
    {
        // TODO: Implement hand tracking using XR Toolkit or Unity Input System.
        return false;
    }

    public void SpawnOrRecycleMonitor()
    {
        if (monitorPool.Count > 0)
        {
            GameObject monitor = monitorPool.Dequeue();
            monitor.transform.position = spawnPoint.position;
            monitor.transform.rotation = spawnPoint.rotation;
            monitor.SetActive(true);
            Debug.Log("Monitor spawned from pool.");
        }
        else
        {
            Debug.LogWarning("No available monitors in pool; consider increasing maxMonitors.");
        }
    }

    public void DespawnMonitor(GameObject monitor)
    {
        if (monitor != null)
        {
            monitor.SetActive(false);
            monitorPool.Enqueue(monitor);
            Debug.Log("Monitor returned to pool.");
        }
    }
}
