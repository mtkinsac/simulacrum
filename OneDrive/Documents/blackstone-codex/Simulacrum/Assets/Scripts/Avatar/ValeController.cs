using UnityEngine;

public class ValeController : MonoBehaviour
{
    public Animator animator;

    public void TriggerSpeakAnimation()
    {
        if (animator != null)
            animator.SetTrigger("Speak");
    }

    public void ActivateAvatar()
    {
        gameObject.SetActive(true);
        // TODO: Add spawn FX or shimmer effect.
    }

    public void DeactivateAvatar()
    {
        gameObject.SetActive(false);
    }
}
