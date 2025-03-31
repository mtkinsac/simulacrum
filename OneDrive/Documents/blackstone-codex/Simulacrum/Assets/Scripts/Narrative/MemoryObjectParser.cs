using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class MemoryObjectParser : MonoBehaviour
{
    [Header("Parser Settings")]
    [Tooltip("Relative folder path from Application.dataPath where MemoryObject files are located (e.g., 'Docs/Narrative/MemoryObjects').")]
    public string memoryFolderPath = "Docs/Narrative/MemoryObjects";

    [Tooltip("File extension to parse (e.g., .txt)")]
    public string fileExtension = ".txt";

    // Dictionary to hold parsed MemoryObjectData, keyed by memoryId.
    public Dictionary<string, MemoryObjectData> memoryObjects = new Dictionary<string, MemoryObjectData>();

    void Start()
    {
        ParseMemoryObjects();
    }

    public void ParseMemoryObjects()
    {
        string fullPath = Path.Combine(Application.dataPath, memoryFolderPath);
        if (!Directory.Exists(fullPath))
        {
            Debug.LogError("MemoryObjectParser: Directory does not exist: " + fullPath);
            return;
        }
        string[] files = Directory.GetFiles(fullPath, "*" + fileExtension, SearchOption.TopDirectoryOnly);
        foreach (string file in files)
        {
            MemoryObjectData data = ParseFile(file);
            if (data != null && !string.IsNullOrEmpty(data.memoryId))
            {
                memoryObjects[data.memoryId] = data;
                Debug.Log("MemoryObjectParser: Parsed memory object " + data.memoryId);
            }
        }
    }

    private MemoryObjectData ParseFile(string filePath)
    {
        try
        {
            string[] lines = File.ReadAllLines(filePath);
            if (lines.Length == 0)
            {
                Debug.LogWarning("MemoryObjectParser: File is empty: " + filePath);
                return null;
            }
            MemoryObjectData data = new MemoryObjectData();

            // Expecting the file to start with "MemoryObject: <Name>"
            if (!lines[0].StartsWith("MemoryObject:"))
            {
                Debug.LogWarning("MemoryObjectParser: File does not start with 'MemoryObject:' - " + filePath);
                return null;
            }
            data.memoryId = lines[0].Substring("MemoryObject:".Length).Trim();

            // Parse subsequent lines for tags.
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (line.StartsWith("[Emotion]"))
                {
                    data.emotionTag = ExtractValue(line);
                }
                else if (line.StartsWith("[Intensity]"))
                {
                    data.intensityTag = ExtractValue(line);
                }
                else if (line.StartsWith("[RealmTie]"))
                {
                    data.realmTie = ExtractValue(line);
                }
                else if (line.StartsWith("[AssociatedCodex]"))
                {
                    data.associatedCodex = ExtractValue(line);
                }
                else if (line.StartsWith("REFLECTION:"))
                {
                    data.reflectionLine = line.Substring("REFLECTION:".Length).Trim();
                }
                // Additional fields (e.g., title, body) can be parsed here as needed.
            }
            return data;
        }
        catch (System.Exception ex)
        {
            Debug.LogError("MemoryObjectParser: Failed to parse file " + filePath + " - " + ex.Message);
            return null;
        }
    }

    private string ExtractValue(string line)
    {
        int index = line.IndexOf("=");
        if (index >= 0 && index < line.Length - 1)
        {
            return line.Substring(index + 1).Trim();
        }
        return "";
    }
}
