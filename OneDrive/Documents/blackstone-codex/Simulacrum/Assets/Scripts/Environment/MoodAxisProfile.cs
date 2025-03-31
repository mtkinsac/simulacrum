using UnityEngine;

public enum MoodAxisType
{
    Sadness,
    Joy,
    Awe,
    Curiosity
}

[CreateAssetMenu(fileName = "MoodAxisProfile", menuName = "Simulacrum/MoodAxisProfile")]
public class MoodAxisProfile : ScriptableObject
{
    [Header("Realm")]
    public string realmName;

    [Header("Optional Gradient Overrides")]
    public Gradient sadnessOverride;
    public Gradient joyOverride;
    public Gradient aweOverride;
    public Gradient curiosityOverride;

    [Header("Optional Multiplier Overrides")]
    public float sadnessLightMultiplierOverride = -1f;
    public float joyLightMultiplierOverride = -1f;
    public float aweEffectMultiplierOverride = -1f;
    public float curiosityAudioMultiplierOverride = -1f;

    public Gradient GetGradientOverride(MoodAxisType axisType)
    {
        return axisType switch
        {
            MoodAxisType.Sadness => sadnessOverride,
            MoodAxisType.Joy => joyOverride,
            MoodAxisType.Awe => aweOverride,
            MoodAxisType.Curiosity => curiosityOverride,
            _ => null,
        };
    }

    public float GetMultiplier(MoodAxisType axisType, AmbientMoodMap globalMap)
    {
        return axisType switch
        {
            MoodAxisType.Sadness => sadnessLightMultiplierOverride > 0 ? sadnessLightMultiplierOverride : globalMap.sadnessLightMultiplier,
            MoodAxisType.Joy => joyLightMultiplierOverride > 0 ? joyLightMultiplierOverride : globalMap.joyLightMultiplier,
            MoodAxisType.Awe => aweEffectMultiplierOverride > 0 ? aweEffectMultiplierOverride : globalMap.aweEffectMultiplier,
            MoodAxisType.Curiosity => curiosityAudioMultiplierOverride > 0 ? curiosityAudioMultiplierOverride : globalMap.curiosityAudioMultiplier,
            _ => 1f,
        };
    }
}
