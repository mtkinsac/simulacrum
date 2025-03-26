using System.Collections.Generic;
using UnityEngine;

public class PrefabRegistry : MonoBehaviour
{
    public static PrefabRegistry Instance;
    public Dictionary<string, GameObject> prefabDictionary = new Dictionary<string, GameObject>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("PrefabRegistry initialized.");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterPrefab(string key, GameObject prefab)
    {
        if (!prefabDictionary.ContainsKey(key))
        {
            prefabDictionary.Add(key, prefab);
            Debug.Log("Prefab registered: " + key);
        }
        else
        {
            Debug.LogWarning("Prefab already registered: " + key);
        }
    }

    public GameObject GetPrefab(string key)
    {
        if (prefabDictionary.ContainsKey(key))
        {
            return prefabDictionary[key];
        }
        else
        {
            Debug.LogError("Prefab not found: " + key);
            return null;
        }
    }
}
