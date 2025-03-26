using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticFeedbackController : MonoBehaviour
{
    public XRBaseController controller;

    public void TriggerHapticFeedback(float amplitude, float duration)
    {
        if (controller != null)
        {
            controller.SendHapticImpulse(amplitude, duration);
        }
        else
        {
            Debug.Log("Haptic feedback: Amplitude " + amplitude + ", Duration " + duration);
        }
    }
}
