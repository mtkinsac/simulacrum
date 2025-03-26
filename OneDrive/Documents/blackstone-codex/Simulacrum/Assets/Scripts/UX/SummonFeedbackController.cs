using UnityEngine;

public class SummonFeedbackController : MonoBehaviour
{
    public ParticleSystem summonParticles;
    public AudioSource summonAudio;

    public void TriggerFeedback()
    {
        if (summonParticles != null)
            summonParticles.Play();
        if (summonAudio != null)
            summonAudio.Play();
        Debug.Log("SummonFeedbackController: Feedback triggered - shimmer and glow effects.");
    }
}
