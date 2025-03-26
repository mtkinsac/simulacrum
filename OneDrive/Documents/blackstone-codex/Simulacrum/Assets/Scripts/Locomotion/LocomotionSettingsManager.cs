using UnityEngine;

public enum LocomotionMode
{
    Natural,
    Teleportation,
    Smooth
}

public class LocomotionSettingsManager : MonoBehaviour
{
    public static LocomotionSettingsManager Instance;

    public LocomotionMode currentMode = LocomotionMode.Natural;
    public float movementSpeed = 2.0f;
    public float sensitivity = 1.0f;
    public float hapticFeedbackStrength = 0.5f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // TODO: Load saved settings from PlayerPrefs.
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
