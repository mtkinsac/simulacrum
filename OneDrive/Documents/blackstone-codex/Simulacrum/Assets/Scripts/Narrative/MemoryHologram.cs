using UnityEngine;

public class MemoryHologram : MonoBehaviour
{
    [Header("Hologram Settings")]
    public AudioSource hologramVoice;
    public Texture hologramImage;
    public ParticleSystem hologramFX;
    public string memoryText;

    void Start()
    {
        if (hologramFX != null)
            hologramFX.Play();
        Debug.Log("MemoryHologram initialized with memory: " + memoryText);
    }

    public void PlayMemory()
    {
        if (hologramVoice != null)
            hologramVoice.Play();
        Debug.Log("MemoryHologram: Playing memory - " + memoryText);
    }
}
