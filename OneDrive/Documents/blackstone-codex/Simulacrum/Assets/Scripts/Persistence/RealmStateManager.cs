using UnityEngine;
using System.Collections.Generic;

public class RealmStateManager : MonoBehaviour
{
    public static RealmStateManager Instance { get; private set; }

    public string CurrentRealmID { get; private set; }

    // Optional: Track unlocked destinations if needed later
    private HashSet<string> unlockedDestinations = new HashSet<string>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetCurrentRealm(string realmID)
    {
        CurrentRealmID = realmID;
    }

    public bool IsInRealm(string realmID)
    {
        return CurrentRealmID == realmID;
    }

    public void UnlockDestination(string destinationID)
    {
        if (!unlockedDestinations.Contains(destinationID))
        {
            unlockedDestinations.Add(destinationID);
            Debug.Log($"RealmStateManager: Destination '{destinationID}' unlocked.");
        }
        else
        {
            Debug.Log($"RealmStateManager: Destination '{destinationID}' already unlocked.");
        }
    }

    public bool IsDestinationUnlocked(string destinationID)
    {
        return unlockedDestinations.Contains(destinationID);
    }
}
