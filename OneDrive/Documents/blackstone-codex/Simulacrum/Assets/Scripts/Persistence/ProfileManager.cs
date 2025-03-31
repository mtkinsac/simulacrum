using UnityEngine;
using System.IO;
using System;

public class ProfileManager : MonoBehaviour
{
    private const string ProfileFileName = "userprofile.json";

    public UserProfile currentProfile;

    void Awake()
    {
        LoadProfile();
    }

    public void SaveProfile()
    {
        try
        {
            string json = JsonUtility.ToJson(currentProfile, true);
            File.WriteAllText(GetProfileFilePath(), json);
            Debug.Log("Profile saved.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Failed to save profile: " + ex.Message);
        }
    }

    public void LoadProfile()
    {
        try
        {
            string path = GetProfileFilePath();
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                currentProfile = JsonUtility.FromJson<UserProfile>(json);
                Debug.Log("Profile loaded.");
            }
            else
            {
                currentProfile = new UserProfile()
                {
                    userName = "NewUser",
                    lastSession = DateTime.Now.ToString(),
                    preferredLayout = "QWERTY"
                };
                SaveProfile();
                Debug.Log("New profile created.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Failed to load profile: " + ex.Message);
        }
    }

    private string GetProfileFilePath()
    {
        return Path.Combine(Application.persistentDataPath, ProfileFileName);
    }
}
