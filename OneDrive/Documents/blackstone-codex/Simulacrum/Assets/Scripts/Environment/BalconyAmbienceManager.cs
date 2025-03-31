using UnityEngine;

public class BalconyAmbienceManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource windAudio;
    public AudioSource chimeAudio;

    [Header("Skybox Settings")]
    public Material daySkybox;
    public Material nightSkybox;
    public float transitionSpeed = 0.5f;

    [Header("Time of Day Simulation")]
    [Range(0f, 24f)]
    public float hourOfDay = 21f;
    public float timeScale = 0.1f;

    private float currentBlend = 0f;

    void Update()
    {
        hourOfDay += Time.deltaTime * timeScale;
        if (hourOfDay >= 24f) hourOfDay -= 24f;

        float targetBlend = (hourOfDay >= 6f && hourOfDay < 18f) ? 0f : 1f;
        currentBlend = Mathf.Lerp(currentBlend, targetBlend, transitionSpeed * Time.deltaTime);

        RenderSettings.skybox.Lerp(daySkybox, nightSkybox, currentBlend);
        DynamicGI.UpdateEnvironment();

        if (windAudio != null)
        {
            windAudio.volume = 0.4f + 0.2f * currentBlend;
        }
        if (chimeAudio != null)
        {
            chimeAudio.pitch = 1.0f + 0.1f * currentBlend;
        }
    }
}
