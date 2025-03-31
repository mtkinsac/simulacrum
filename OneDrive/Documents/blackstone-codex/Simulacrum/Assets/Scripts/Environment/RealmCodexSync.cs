using UnityEngine;

// -------------------------
// 1. RealmCodexSync.cs
// -------------------------
public class RealmCodexSync : MonoBehaviour
{
    [Header("Realm Settings")]
    [Tooltip("Which realm this environment object belongs to, e.g., 'WhisperingWoods'.")]
    public string realmName = "WhisperingWoods";

    void OnEnable()
    {
        CodexRegistrationManager.OnEntryRegistered += HandleCodexEntryRegistered;
    }

    void OnDisable()
    {
        CodexRegistrationManager.OnEntryRegistered -= HandleCodexEntryRegistered;
    }

    private void HandleCodexEntryRegistered(MemoryEchoData data)
    {
        if (data == null || data.additionalText == null) return;
        string content = data.additionalText.text;
        if (content.Contains($"[Realm: {realmName}]"))
        {
            Debug.Log($"RealmCodexSync: Found new Codex entry for realm '{realmName}': {data.title}");
            UnlockRealmSpecificFX();
        }
    }

    private void UnlockRealmSpecificFX()
    {
        Debug.Log($"RealmCodexSync: Unlocking special FX for realm '{realmName}'.");
        // Add FX triggers here.
    }
}
