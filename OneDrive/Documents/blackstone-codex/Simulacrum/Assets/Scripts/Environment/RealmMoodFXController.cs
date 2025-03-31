using UnityEngine;

public class RealmMoodFXController : MonoBehaviour
{
    public string realmName = "WhisperingWoods";
    public AmbientMoodMap moodMap;
    public Light mainLight;
    public ParticleSystem realmParticles;
    public AudioSource realmAudio;

    private ParticleSystem.EmissionModule emissionModule;
    private Color baseLightColor;
    private float baseLightIntensity;
    private float baseAudioVolume;

    void Start()
    {
        if (mainLight != null)
        {
            baseLightColor = mainLight.color;
            baseLightIntensity = mainLight.intensity;
        }
        if (realmAudio != null)
            baseAudioVolume = realmAudio.volume;

        if (realmParticles != null)
            emissionModule = realmParticles.emission;
    }

    void Update()
    {
        if (MemoryMoodManager.Instance == null || moodMap == null) return;

        float sadness = MemoryMoodManager.Instance.sadness;
        float joy = MemoryMoodManager.Instance.joy;
        float awe = MemoryMoodManager.Instance.awe;
        float curiosity = MemoryMoodManager.Instance.curiosity;

        if (mainLight != null)
        {
            Color sadColor = moodMap.GetSadnessColor(sadness);
            Color joyColor = moodMap.GetJoyColor(joy);
            Color finalColor = Color.Lerp(sadColor, joyColor, joy / (sadness + joy + 0.01f));
            float intensity = baseLightIntensity * Mathf.Max(0.1f, (1f - moodMap.sadnessLightMultiplier * (sadness / 100f) + moodMap.joyLightMultiplier * (joy / 100f)));

            mainLight.color = finalColor;
            mainLight.intensity = Mathf.Clamp(intensity, 0f, 5f);
        }

        if (realmParticles != null)
            emissionModule.rateOverTime = moodMap.aweEffectMultiplier * (awe / 100f) * 10f;

        if (realmAudio != null)
        {
            float cFactor = moodMap.curiosityAudioMultiplier * (curiosity / 100f);
            realmAudio.volume = Mathf.Clamp(baseAudioVolume + cFactor, 0f, 1f);
        }
    }
}
