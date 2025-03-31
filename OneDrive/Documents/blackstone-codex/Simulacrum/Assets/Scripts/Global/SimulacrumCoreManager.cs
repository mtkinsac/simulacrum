using UnityEngine;
using System.Collections.Generic;

public class SimulacrumCoreManager : MonoBehaviour
{
    private static SimulacrumCoreManager _instance;
    public static SimulacrumCoreManager Instance => _instance;

    private Dictionary<string, object> registeredModules = new Dictionary<string, object>();

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void RegisterModule(string moduleName, object moduleInstance)
    {
        if (!registeredModules.ContainsKey(moduleName))
        {
            registeredModules.Add(moduleName, moduleInstance);
            Debug.Log($"[SimulacrumCoreManager] Registered module: {moduleName}");
        }
        else
        {
            Debug.LogWarning($"[SimulacrumCoreManager] Module already registered: {moduleName}");
        }
    }

    public T GetModule<T>(string moduleName)
    {
        if (registeredModules.TryGetValue(moduleName, out object module))
        {
            return (T)module;
        }

        Debug.LogWarning($"[SimulacrumCoreManager] Module not found: {moduleName}");
        return default;
    }
}
