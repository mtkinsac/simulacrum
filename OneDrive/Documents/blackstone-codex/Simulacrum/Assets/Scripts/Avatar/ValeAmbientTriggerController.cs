using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ValeAmbientTriggerController : MonoBehaviour
{
    private Animator valeAnimator;

    void Awake()
    {
        valeAnimator = GetComponent<Animator>();
    }

    public void TriggerIdlePacing()
    {
        if (valeAnimator != null)
        {
            valeAnimator.SetTrigger("IdlePace");
            Debug.Log("ValeAmbientTriggerController: Triggered idle pacing.");
        }
    }

    public void TriggerIdleFireGaze()
    {
        if (valeAnimator != null)
        {
            valeAnimator.SetTrigger("IdleFireGaze");
            Debug.Log("ValeAmbientTriggerController: Triggered idle near fire.");
        }
    }
}
