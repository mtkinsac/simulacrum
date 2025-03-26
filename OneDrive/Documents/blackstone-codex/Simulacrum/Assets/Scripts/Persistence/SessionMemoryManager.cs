using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class MonitorState
{
    public Vector3 position;
    public Quaternion rotation;
}

[System.Serializable]
public class SessionState
{
    public List<MonitorState> monitorStates = new List<MonitorState>();
    public string keyboardState;
    public bool valeVisible;
    public string lastTTSMessage;
    public List<string> unlockedHolograms = new List<string>(); // MemoryEchoData.id values
    public List<string> viewedCodexEntries = new List<string>();  // For codex tracking
}

public class SessionMemoryManager : MonoBehaviour
{
    private const string SessionStateKey = "SessionState";
    public SessionState currentSessionState = new SessionState();

    void Awake()
    {
        LoadSessionState();
    }

    public void SaveSessionState()
    {
        string json = JsonUtility.ToJson(currentSessionState);
        PlayerPrefs.SetString(SessionStateKey, json);
        PlayerPrefs.Save();
        Debug.Log("SessionMemoryManager: Session state saved.");
    }

    public void LoadSessionState()
    {
        if (PlayerPrefs.HasKey(SessionStateKey))
        {
            string json = PlayerPrefs.GetString(SessionStateKey);
            currentSessionState = JsonUtility.FromJson<SessionState>(json);
            Debug.Log("SessionMemoryManager: Session state loaded.");
        }
        else
        {
            Debug.Log("SessionMemoryManager: No session state found; starting new session.");
            currentSessionState = new SessionState();
        }
    }

    public void ClearSessionState()
    {
        PlayerPrefs.DeleteKey(SessionStateKey);
        currentSessionState = new SessionState();
        Debug.Log("SessionMemoryManager: Session state cleared.");
    }

    public void ReplayLastMemory()
    {
        if (currentSessionState.unlockedHolograms.Count > 0)
        {
            string lastMemory = currentSessionState.unlockedHolograms[currentSessionState.unlockedHolograms.Count - 1];
            Debug.Log("SessionMemoryManager: Replaying last memory: " + lastMemory);
            EventManager.ValeSpeakRequest("Replaying memory: " + lastMemory);
        }
        else
        {
            Debug.LogWarning("SessionMemoryManager: No memories unlocked to replay.");
        }
    }
}
