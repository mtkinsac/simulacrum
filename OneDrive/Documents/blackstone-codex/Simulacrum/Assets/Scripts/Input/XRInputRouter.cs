using UnityEngine;
using UnityEngine.InputSystem;

public class XRInputRouter : MonoBehaviour
{
    public InputAction summonAction;
    public InputAction keyboardToggleAction;

    void OnEnable()
    {
        if (summonAction != null)
        {
            summonAction.Enable();
            summonAction.performed += OnSummonPerformed;
        }
        if (keyboardToggleAction != null)
        {
            keyboardToggleAction.Enable();
            keyboardToggleAction.performed += OnKeyboardTogglePerformed;
        }
    }

    void OnDisable()
    {
        if (summonAction != null)
        {
            summonAction.performed -= OnSummonPerformed;
            summonAction.Disable();
        }
        if (keyboardToggleAction != null)
        {
            keyboardToggleAction.performed -= OnKeyboardTogglePerformed;
            keyboardToggleAction.Disable();
        }
    }

    private void OnSummonPerformed(InputAction.CallbackContext context)
    {
        EventManager.SummonRequest();
    }

    private void OnKeyboardTogglePerformed(InputAction.CallbackContext context)
    {
        bool toggleState = context.ReadValue<float>() > 0.5f;
        EventManager.KeyboardToggled(toggleState);
    }
}
