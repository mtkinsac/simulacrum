using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private const string LocomotionModeKey = "LocomotionMode";
    private const string KeyboardLayoutKey = "KeyboardLayout";
    private const string AudioVolumeKey = "AudioVolume";
    private const string WristUIToggleKey = "WristUIToggle";

    public LocomotionMode locomotionMode = LocomotionMode.Natural;
    public string keyboardLayout = "QWERTY";
    public float audioVolume = 1.0f;
    public bool wristUIToggle = true;

    void Awake()
    {
        LoadSettings();
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt(LocomotionModeKey, (int)locomotionMode);
        PlayerPrefs.SetString(KeyboardLayoutKey, keyboardLayout);
        PlayerPrefs.SetFloat(AudioVolumeKey, audioVolume);
        PlayerPrefs.SetInt(WristUIToggleKey, wristUIToggle ? 1 : 0);
        PlayerPrefs.Save();
        Debug.Log("Settings saved.");
    }

    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey(LocomotionModeKey))
            locomotionMode = (LocomotionMode)PlayerPrefs.GetInt(LocomotionModeKey);
        if (PlayerPrefs.HasKey(KeyboardLayoutKey))
            keyboardLayout = PlayerPrefs.GetString(KeyboardLayoutKey);
        if (PlayerPrefs.HasKey(AudioVolumeKey))
            audioVolume = PlayerPrefs.GetFloat(AudioVolumeKey);
        if (PlayerPrefs.HasKey(WristUIToggleKey))
            wristUIToggle = PlayerPrefs.GetInt(WristUIToggleKey) == 1;

        Debug.Log("Settings loaded.");
    }
}
