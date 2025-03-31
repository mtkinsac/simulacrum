using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Represents a single codex entry, parsed from a text asset.
/// </summary>
public class CodexEntry
{
    public string codexID;
    public string title;
    public string content;
    public List<string> tags = new List<string>();
    public string summary;
}

/// <summary>
/// This class auto-registers codex entries by scanning a designated Resources folder
/// for text assets named with a "CodexEntry_" or "MemoryObject_" prefix, then parses them
/// for metadata (Title, Tags, Summary) and full content.
/// </summary>
public class CodexParser : MonoBehaviour
{
    [Header("Configuration")]
    [Tooltip("Relative path inside Resources where codex files are stored (e.g., 'CodexEntries')")]
    public string codexFolderPath = "CodexEntries";

    [Header("Registered Codex Entries")]
    [Tooltip("All parsed and registered codex entries will be stored here.")]
    public List<CodexEntry> registeredEntries = new List<CodexEntry>();

    void Awake()
    {
        AutoRegisterCodexEntries();
    }

    /// <summary>
    /// Automatically loads all TextAssets from the designated folder and registers them.
    /// </summary>
    public void AutoRegisterCodexEntries()
    {
        TextAsset[] entries = Resources.LoadAll<TextAsset>(codexFolderPath);
        Debug.Log("CodexParser: Found " + entries.Length + " text assets in '" + codexFolderPath + "'.");

        foreach (TextAsset textAsset in entries)
        {
            CodexEntry entry = ParseCodexEntry(textAsset);
            if (entry != null)
            {
                registeredEntries.Add(entry);
                Debug.Log("CodexParser: Registered codex entry with ID: " + entry.codexID);
            }
        }
    }

    /// <summary>
    /// Parses a single TextAsset into a CodexEntry.
    /// Expected file naming: "CodexEntry_<ID>" or "MemoryObject_<ID>"
    /// Expected file format:
    /// 
    /// Title: The Title of This Entry
    /// Tags: Tag1, Tag2, Tag3
    /// Summary: A brief description.
    ///
    /// [Blank line]
    /// Full content of the codex entry...
    /// </summary>
    /// <param name="textAsset">The TextAsset to parse.</param>
    /// <returns>A CodexEntry if parsing succeeds; otherwise, null.</returns>
    public CodexEntry ParseCodexEntry(TextAsset textAsset)
    {
        // Validate file naming convention.
        string filename = textAsset.name; // No extension.
        if (!(filename.StartsWith("CodexEntry_") || filename.StartsWith("MemoryObject_")))
        {
            Debug.LogWarning("CodexParser: Skipping file '" + filename + "' as it does not follow naming conventions.");
            return null;
        }

        CodexEntry entry = new CodexEntry();

        // Extract codexID from filename.
        int underscoreIndex = filename.IndexOf("_");
        if (underscoreIndex >= 0 && filename.Length > underscoreIndex + 1)
        {
            entry.codexID = filename.Substring(underscoreIndex + 1);
        }
        else
        {
            Debug.LogWarning("CodexParser: Unable to extract codex ID from filename '" + filename + "'.");
            return null;
        }

        // Split the file into lines.
        string[] lines = textAsset.text.Split(new char[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
        int index = 0;

        // Parse header metadata until a blank line is encountered.
        while (index < lines.Length)
        {
            string line = lines[index].Trim();
            if (string.IsNullOrEmpty(line))
            {
                // Blank line marks the end of header.
                index++;
                break;
            }

            // Process header key-value pairs.
            if (line.StartsWith("Title:", System.StringComparison.OrdinalIgnoreCase))
            {
                entry.title = line.Substring("Title:".Length).Trim();
            }
            else if (line.StartsWith("Tags:", System.StringComparison.OrdinalIgnoreCase))
            {
                string tagLine = line.Substring("Tags:".Length).Trim();
                string[] tagArray = tagLine.Split(new char[] { ',', ';' }, System.StringSplitOptions.RemoveEmptyEntries);
                foreach (string tag in tagArray)
                {
                    entry.tags.Add(tag.Trim());
                }
            }
            else if (line.StartsWith("Summary:", System.StringComparison.OrdinalIgnoreCase))
            {
                entry.summary = line.Substring("Summary:".Length).Trim();
            }
            // Additional header fields can be added here.
            index++;
        }

        // The remaining lines form the full content.
        if (index < lines.Length)
        {
            entry.content = string.Join("\n", lines, index, lines.Length - index);
        }
        else
        {
            entry.content = "";
        }

        return entry;
    }
}
