using UnityEngine;
using UnityEngine.InputSystem;

public class HandTrackingInput : MonoBehaviour
{
    public bool IsPinching()
    {
        return Keyboard.current.spaceKey.isPressed;
    }

    public bool IsSummonGesture()
    {
        return Keyboard.current.sKey.wasPressedThisFrame;
    }
}
