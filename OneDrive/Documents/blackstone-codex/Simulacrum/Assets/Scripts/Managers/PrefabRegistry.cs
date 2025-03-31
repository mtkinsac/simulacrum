using UnityEngine;
using System.Collections.Generic;

public class PrefabRegistry : MonoBehaviour
{
    // Singleton instance for global access
    public static PrefabRegistry Instance;

    // Dictionary for registering prefab references by key
    private Dictionary<string, GameObject> prefabDictionary = new Dictionary<string, GameObject>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Optionally: DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Register a prefab with a unique key
    public void RegisterPrefab(string key, GameObject prefab)
    {
        if (!prefabDictionary.ContainsKey(key))
        {
            prefabDictionary.Add(key, prefab);
            Debug.Log("PrefabRegistry: Registered prefab: " + key);
        }
        else
        {
            Debug.LogWarning("PrefabRegistry: Prefab already registered under key: " + key);
        }
    }

    // Retrieve a prefab by its key
    public GameObject GetPrefab(string key)
    {
        if (prefabDictionary.TryGetValue(key, out GameObject prefab))
        {
            return prefab;
        }
        Debug.LogError("PrefabRegistry: Prefab not found for key: " + key);
        return null;
    }
}
