using UnityEngine;

public class StoryPortal : MonoBehaviour
{
    [Header("Portal Settings")]
    public string narrativeSegmentID;
    public ParticleSystem portalFX;
    public AudioSource portalAudio;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            ActivatePortal();
    }

    void ActivatePortal()
    {
        if (portalFX != null)
            portalFX.Play();
        if (portalAudio != null)
            portalAudio.Play();

        // TODO: Load immersive narrative segment based on narrativeSegmentID.
        Debug.Log("StoryPortal activated: Loading narrative segment " + narrativeSegmentID);
    }
}
