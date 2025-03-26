using UnityEngine;
using System.Collections.Generic;

public class CodexRegistrationManager : MonoBehaviour
{
    public static CodexRegistrationManager Instance;
    private Dictionary<string, MemoryEchoData> registeredEntries = new Dictionary<string, MemoryEchoData>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("CodexRegistrationManager initialized.");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterEntry(MemoryEchoData data, bool autoRegister = true)
    {
        if (data == null)
        {
            Debug.LogWarning("CodexRegistrationManager: Null data cannot be registered.");
            return;
        }
        if (!registeredEntries.ContainsKey(data.id))
        {
            if (autoRegister || !data.requiresApproval)
            {
                registeredEntries.Add(data.id, data);
                Debug.Log("CodexRegistrationManager: Registered entry: " + data.title);
            }
            else
            {
                Debug.Log("CodexRegistrationManager: Entry requires manual approval: " + data.title);
            }
        }
        else
        {
            Debug.LogWarning("CodexRegistrationManager: Entry already registered: " + data.title);
        }
    }

    public MemoryEchoData GetEntry(string id)
    {
        if (registeredEntries.ContainsKey(id))
        {
            return registeredEntries[id];
        }
        else
        {
            Debug.LogWarning("CodexRegistrationManager: Entry not found: " + id);
            return null;
        }
    }
}
