#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
using UnityEngine;
using System;

public class SummonableContentEvents : MonoBehaviour
{
#if ENABLE_INPUT_SYSTEM
    public InputAction summonAction;
#endif

    public static event Action OnSummonContentRequest;

    void OnEnable()
    {
#if ENABLE_INPUT_SYSTEM
        summonAction?.Enable();
#endif
    }

    void OnDisable()
    {
#if ENABLE_INPUT_SYSTEM
        summonAction?.Disable();
#endif
    }

    void Update()
    {
#if ENABLE_INPUT_SYSTEM
        if (summonAction != null && summonAction.triggered)
        {
            Debug.Log("Summon triggered!");
            OnSummonContentRequest?.Invoke();
        }
#endif
    }
}
