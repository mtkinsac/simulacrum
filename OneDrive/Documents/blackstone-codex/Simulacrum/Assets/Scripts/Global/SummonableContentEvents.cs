using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class SummonableContentEvents : MonoBehaviour
{
    public static event Action OnSummonContentRequest;
    public InputAction summonContentAction;

    void OnEnable()
    {
        if (summonContentAction != null)
        {
            summonContentAction.Enable();
            summonContentAction.performed += HandleSummonContent;
        }
        else
        {
            Debug.LogWarning("SummonableContentEvents: summonContentAction is not assigned.");
        }
    }

    void OnDisable()
    {
        if (summonContentAction != null)
        {
            summonContentAction.performed -= HandleSummonContent;
            summonContentAction.Disable();
        }
    }

    private void HandleSummonContent(InputAction.CallbackContext context)
    {
        OnSummonContentRequest?.Invoke();
        Debug.Log("SummonableContentEvents: Summon content request triggered.");
    }
}
