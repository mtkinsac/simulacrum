using UnityEngine;
using System;

public class WandGestureListener : MonoBehaviour
{
    // Event to notify when the capital "G" gesture is detected
    public static event Action OnGandorGestureDetected;

    void Update()
    {
        // Fallback: simulate gesture detection with the "G" key
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Gandor gesture (G) detected via keyboard input.");
            OnGandorGestureDetected?.Invoke();
        }

        // TODO: Implement VR wand motion tracking to detect a "G" shape gesture
    }
}
