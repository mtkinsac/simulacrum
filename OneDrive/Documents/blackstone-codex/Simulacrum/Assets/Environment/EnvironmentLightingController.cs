using UnityEngine;

public class EnvironmentLightingController : MonoBehaviour
{
    public Light deskLampLight;
    public Light fireplaceLight;
    [Range(0f, 1f)]
    public float flickerIntensityRange = 0.2f;
    public float flickerSpeed = 0.1f;
    private float baseDeskLampIntensity;
    private float baseFireplaceIntensity;

    void Start()
    {
        if (deskLampLight != null)
            baseDeskLampIntensity = deskLampLight.intensity;
        if (fireplaceLight != null)
            baseFireplaceIntensity = fireplaceLight.intensity;
    }

    void Update()
    {
        if (deskLampLight != null)
            deskLampLight.intensity = baseDeskLampIntensity + Random.Range(-flickerIntensityRange, flickerIntensityRange);
        if (fireplaceLight != null)
            fireplaceLight.intensity = baseFireplaceIntensity + Random.Range(-flickerIntensityRange, flickerIntensityRange);
    }
}
