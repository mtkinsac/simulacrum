using UnityEngine;
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

    void Awake()
    {
        LoadDestinations();
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

    public DestinationData GetDestinationByID(string id)
    {
        if (destinationDictionary.ContainsKey(id))
            return destinationDictionary[id];
        Debug.LogWarning("DestinationLoader: Destination not found: " + id);
        return null;
    }
}
