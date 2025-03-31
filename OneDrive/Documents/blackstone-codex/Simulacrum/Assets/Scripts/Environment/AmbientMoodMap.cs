using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AmbientMoodMap", menuName = "Simulacrum/AmbientMoodMap")]
public class AmbientMoodMap : ScriptableObject
{
    [Header("Global Mood Gradients (Default)")]
    public Gradient sadnessGradient;
    public Gradient joyGradient;
    public Gradient aweGradient;
    public Gradient curiosityGradient;

    [Header("Global Scaling Multipliers")]
    public float sadnessLightMultiplier = 0.5f;
    public float joyLightMultiplier = 1.2f;
    public float aweEffectMultiplier = 1.5f;
    public float curiosityAudioMultiplier = 1.1f;

    [Header("Realm Overrides")]
    public List<MoodAxisProfile> realmMoodProfiles = new List<MoodAxisProfile>();

    public Color GetSadnessColor(float sadness, string realmName = "")
    {
        Gradient overrideGrad = GetOverrideGradient(realmName, MoodAxisType.Sadness);
        return EvaluateGradient(overrideGrad != null ? overrideGrad : sadnessGradient, sadness);
    }

    public Color GetJoyColor(float joy, string realmName = "")
    {
        Gradient overrideGrad = GetOverrideGradient(realmName, MoodAxisType.Joy);
        return EvaluateGradient(overrideGrad != null ? overrideGrad : joyGradient, joy);
    }

    public Color GetAweColor(float awe, string realmName = "")
    {
        Gradient overrideGrad = GetOverrideGradient(realmName, MoodAxisType.Awe);
        return EvaluateGradient(overrideGrad != null ? overrideGrad : aweGradient, awe);
    }

    public Color GetCuriosityColor(float curiosity, string realmName = "")
    {
        Gradient overrideGrad = GetOverrideGradient(realmName, MoodAxisType.Curiosity);
        return EvaluateGradient(overrideGrad != null ? overrideGrad : curiosityGradient, curiosity);
    }

    public float GetMultiplier(MoodAxisType axisType, string realmName = "")
    {
        MoodAxisProfile realmProfile = realmMoodProfiles.Find(x => x.realmName == realmName);
        if (realmProfile != null)
        {
            return realmProfile.GetMultiplier(axisType, this);
        }
        return GetGlobalMultiplier(axisType);
    }

    private Gradient GetOverrideGradient(string realmName, MoodAxisType axisType)
    {
        if (string.IsNullOrEmpty(realmName)) return null;
        MoodAxisProfile realmProfile = realmMoodProfiles.Find(x => x.realmName == realmName);
        return realmProfile?.GetGradientOverride(axisType);
    }

    private Color EvaluateGradient(Gradient grad, float axisValue)
    {
        float t = Mathf.Clamp01(axisValue / 100f);
        return grad.Evaluate(t);
    }

    private float GetGlobalMultiplier(MoodAxisType axisType)
    {
        return axisType switch
        {
            MoodAxisType.Sadness => sadnessLightMultiplier,
            MoodAxisType.Joy => joyLightMultiplier,
            MoodAxisType.Awe => aweEffectMultiplier,
            MoodAxisType.Curiosity => curiosityAudioMultiplier,
            _ => 1f,
        };
    }
}
