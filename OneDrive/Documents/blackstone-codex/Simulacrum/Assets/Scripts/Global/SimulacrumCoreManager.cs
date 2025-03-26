using System.Collections.Generic;
using UnityEngine;

public class SimulacrumCoreManager : MonoBehaviour
{
    public static SimulacrumCoreManager Instance;
    private Dictionary<string, GameObject> loadedModules = new Dictionary<string, GameObject>();
    public bool devMode = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("SimulacrumCoreManager initialized.");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterModule(string moduleName, GameObject module)
    {
        if (!loadedModules.ContainsKey(moduleName))
        {
            loadedModules.Add(moduleName, module);
            if (devMode) Debug.Log("Module registered: " + moduleName);
        }
        else
        {
            Debug.LogWarning("Module already registered: " + moduleName);
        }
    }

    public void UnregisterModule(string moduleName)
    {
        if (loadedModules.ContainsKey(moduleName))
        {
            loadedModules.Remove(moduleName);
            if (devMode) Debug.Log("Module unregistered: " + moduleName);
        }
        else
        {
            Debug.LogWarning("Module not found: " + moduleName);
        }
    }

    public void PrintRegisteredModules()
    {
        Debug.Log("Registered Modules:");
        foreach (var module in loadedModules)
        {
            Debug.Log(" - " + module.Key);
        }
    }
}
