using UnityEngine;
using System.Collections.Generic;

public class SummonableMonitorController : MonoBehaviour
{
    public static SummonableMonitorController Instance;

    public GameObject deskMonitorPrefab;
    public GameObject fireplaceScreenPrefab;
    public GameObject roundtableMonitorPrefab;
    public GameObject gamingTableMonitorPrefab; // New prefab for gaming table monitors

    // Anchor positions for gaming table monitors (north, south, east, west)
    public Dictionary<string, Vector3> gamingTableAnchors = new Dictionary<string, Vector3>()
    {
        {"north", new Vector3(0, 1.5f, 2)},
        {"south", new Vector3(0, 1.5f, -2)},
        {"east",  new Vector3(2, 1.5f, 0)},
        {"west",  new Vector3(-2, 1.5f, 0)}
    };

    // List to keep track of active monitors with additional tag info
    private List<MonitorInstance> activeMonitors = new List<MonitorInstance>();

    // Helper class to store monitor instance details
    public class MonitorInstance
    {
        public GameObject monitor;
        public string type;
        public string anchor;      // Optional anchor (e.g., for gaming table)
        public string summonTag;   // Optional tag for targeted dismissal

        public MonitorInstance(GameObject monitor, string type, string anchor = "", string summonTag = "")
        {
            this.monitor = monitor;
            this.type = type;
            this.anchor = anchor;
            this.summonTag = summonTag;
        }
    }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Summon a monitor with optional anchor and summonTag
    public void SummonMonitor(string type, string anchor = "", string summonTag = "")
    {
        GameObject prefab = null;
        Vector3 position = Vector3.zero;

        // If summoning for gaming table with an anchor specified
        if (type == "GamingTable" && !string.IsNullOrEmpty(anchor))
        {
            prefab = gamingTableMonitorPrefab;
            if (gamingTableAnchors.ContainsKey(anchor.ToLower()))
                position = gamingTableAnchors[anchor.ToLower()];
            else
            {
                Debug.LogWarning("GamingTable anchor not recognized: " + anchor + ". Using default position.");
                position = Vector3.zero;
            }
        }
        else
        {
            switch (type)
            {
                case "Desk":
                    prefab = deskMonitorPrefab;
                    position = new Vector3(0, 1.5f, 2);
                    break;
                case "Fireplace":
                    prefab = fireplaceScreenPrefab;
                    position = new Vector3(3, 2, 2);
                    break;
                case "Roundtable":
                    prefab = roundtableMonitorPrefab;
                    position = new Vector3(-3, 1.5f, 2);
                    break;
                default:
                    Debug.LogError("Unknown monitor type: " + type);
                    return;
            }
        }

        GameObject monitor = Instantiate(prefab, position, Quaternion.identity);
        activeMonitors.Add(new MonitorInstance(monitor, type, anchor, summonTag));
        Debug.Log("Summoned " + type + " monitor at " + position + " with anchor: " + anchor + " and tag: " + summonTag);
    }

    // Dismiss monitors matching a specific summonTag or type; if both are empty, dismiss all
    public void DismissMonitors(string summonTag = "", string type = "")
    {
        for (int i = activeMonitors.Count - 1; i >= 0; i--)
        {
            var instance = activeMonitors[i];
            if ((!string.IsNullOrEmpty(summonTag) && instance.summonTag == summonTag) ||
                (!string.IsNullOrEmpty(type) && instance.type == type) ||
                (string.IsNullOrEmpty(summonTag) && string.IsNullOrEmpty(type)))
            {
                if (instance.monitor != null)
                    Destroy(instance.monitor);
                activeMonitors.RemoveAt(i);
            }
        }
    }

    // Dismiss all monitors
    public void DismissAllMonitors()
    {
        DismissMonitors();
    }
}
