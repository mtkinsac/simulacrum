using UnityEngine;
using System;

[CreateAssetMenu(fileName = "AmbientMoodMap", menuName = "Simulacrum/AmbientMoodMap")]
public class AmbientMoodMap : ScriptableObject
{
    [Header("Global Mood Colors")]
    public Gradient sadnessGradient;
    public Gradient joyGradient;
    public Gradient aweGradient;
    public Gradient curiosityGradient;

    [Header("Scaling Factors")]
    public float sadnessLightMultiplier = 0.5f;
    public float joyLightMultiplier = 1.2f;
    public float aweEffectMultiplier = 1.5f;
    public float curiosityAudioMultiplier = 1.1f;

    public Color GetSadnessColor(float sadness) => sadnessGradient.Evaluate(Mathf.Clamp01(sadness / 100f));
    public Color GetJoyColor(float joy) => joyGradient.Evaluate(Mathf.Clamp01(joy / 100f));
    public Color GetAweColor(float awe) => aweGradient.Evaluate(Mathf.Clamp01(awe / 100f));
    public Color GetCuriosityColor(float curiosity) => curiosityGradient.Evaluate(Mathf.Clamp01(curiosity / 100f));
}
