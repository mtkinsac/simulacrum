using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

[Serializable]
public class DestinationData
{
    public string destinationID;
    public string sceneName;
    public string destinationName;
    public string narrativeLayer;
    public string description;
    public string summary;
    public string codexReference;
    public string[] tags;
    public string weatherHint;
    public string fxType;
}

[Serializable]
public class DestinationDataList
{
    public DestinationData[] destinations;
}

public class DestinationLoader : MonoBehaviour
{
    public TextAsset destinationJson;
    private Dictionary<string, DestinationData> destinationDictionary = new Dictionary<string, DestinationData>();

    // New: Dictionary mapping destination IDs to ambient descriptions
    private Dictionary<string, string> ambientProfiles = new Dictionary<string, string>();

    void Awake()
    {
        LoadDestinations();
        PreloadAmbientProfiles();
    }

    public void LoadDestinations()
    {
        if (destinationJson == null)
        {
            Debug.LogError("DestinationLoader: DestinationData.json not assigned.");
            return;
        }

        DestinationDataList dataList = JsonUtility.FromJson<DestinationDataList>(destinationJson.text);
        if (dataList != null && dataList.destinations != null)
        {
            foreach (DestinationData dest in dataList.destinations)
            {
                if (!destinationDictionary.ContainsKey(dest.destinationID))
                {
                    destinationDictionary.Add(dest.destinationID, dest);
                }
            }
            Debug.Log("DestinationLoader: Loaded " + destinationDictionary.Count + " destinations.");
        }
        else
        {
            Debug.LogError("DestinationLoader: Failed to parse DestinationData.json.");
        }
    }

    // New: Preload ambient profiles for destinations (could be extended to load from external sources)
    private void PreloadAmbientProfiles()
    {
        ambientProfiles["WhisperingWoods"] = "Shimmering fog, gentle rustle, distant chimes";
        ambientProfiles["GamingNexus"] = "Electronic hum, ambient glow, soft murmurs";
        // Extend with additional ambient data as needed
        Debug.Log("DestinationLoader: Ambient profiles preloaded: " + string.Join(", ", ambientProfiles.Keys));
    }

    public DestinationData GetDestinationByID(string id)
    {
        if (destinationDictionary.ContainsKey(id))
            return destinationDictionary[id];

        Debug.LogWarning("DestinationLoader: Destination not found: " + id);
        return null;
    }

    public void LoadDestination(string id)
    {
        DestinationData destination = GetDestinationByID(id);
        if (destination != null)
        {
            Debug.Log("DestinationLoader: Loading scene '" + destination.sceneName + "' for destination ID: " + id);
            // Optionally log ambient profile information if available
            if (ambientProfiles.ContainsKey(id))
            {
                Debug.Log("Ambient profile: " + ambientProfiles[id]);
            }
            SceneManager.LoadScene(destination.sceneName);
        }
        else
        {
            Debug.LogError("DestinationLoader: Cannot load scene. Destination ID not found: " + id);
        }
    }

    // New: Validate destination ID against both destination data and ambient profiles
    public bool ValidateDestinationID(string id)
    {
        bool existsInData = destinationDictionary.ContainsKey(id);
        bool existsInAmbient = ambientProfiles.ContainsKey(id);
        if (!existsInData)
            Debug.LogWarning("DestinationLoader: Destination data missing for ID: " + id);
        if (!existsInAmbient)
            Debug.LogWarning("DestinationLoader: Ambient profile missing for ID: " + id);
        return existsInData && existsInAmbient;
    }
}
