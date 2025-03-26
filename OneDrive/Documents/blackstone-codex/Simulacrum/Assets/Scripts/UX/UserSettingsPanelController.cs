using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserSettingsPanelController : MonoBehaviour
{
    [Header("Locomotion Mode Toggles")]
    public Toggle naturalToggle;
    public Toggle teleportToggle;
    public Toggle smoothToggle;

    [Header("Locomotion Sliders")]
    public Slider movementSpeedSlider;
    public Slider sensitivitySlider;
    public Slider hapticFeedbackSlider;

    [Header("Display")]
    public TextMeshProUGUI currentModeText;

    void Start()
    {
        if (LocomotionSettingsManager.Instance == null)
        {
            Debug.LogError("UserSettingsPanelController: LocomotionSettingsManager instance not found!");
            return;
        }
        movementSpeedSlider.value = LocomotionSettingsManager.Instance.movementSpeed;
        sensitivitySlider.value = LocomotionSettingsManager.Instance.sensitivity;
        hapticFeedbackSlider.value = LocomotionSettingsManager.Instance.hapticFeedbackStrength;
        UpdateCurrentModeText();
    }

    public void OnLocomotionModeChanged()
    {
        if (naturalToggle == null || teleportToggle == null || smoothToggle == null)
        {
            Debug.LogError("One or more toggles are not assigned in UserSettingsPanelController.");
            return;
        }

        if (naturalToggle.isOn)
            LocomotionSettingsManager.Instance.currentMode = LocomotionMode.Natural;
        else if (teleportToggle.isOn)
            LocomotionSettingsManager.Instance.currentMode = LocomotionMode.Teleportation;
        else if (smoothToggle.isOn)
            LocomotionSettingsManager.Instance.currentMode = LocomotionMode.Smooth;
        UpdateCurrentModeText();
        Debug.Log("Locomotion mode set to: " + LocomotionSettingsManager.Instance.currentMode);
    }

    public void OnMovementSpeedChanged(float value)
    {
        LocomotionSettingsManager.Instance.movementSpeed = value;
        Debug.Log("Movement speed set to: " + value);
    }

    public void OnSensitivityChanged(float value)
    {
        LocomotionSettingsManager.Instance.sensitivity = value;
        Debug.Log("Sensitivity set to: " + value);
    }

    public void OnHapticFeedbackChanged(float value)
    {
        LocomotionSettingsManager.Instance.hapticFeedbackStrength = value;
        Debug.Log("Haptic feedback strength set to: " + value);
    }

    void UpdateCurrentModeText()
    {
        if (currentModeText != null)
            currentModeText.text = "Current Mode: " + LocomotionSettingsManager.Instance.currentMode.ToString();
    }
}
