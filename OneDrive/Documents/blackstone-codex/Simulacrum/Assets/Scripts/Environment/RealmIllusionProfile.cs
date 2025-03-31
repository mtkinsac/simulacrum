using UnityEngine;
using System;

[CreateAssetMenu(fileName = "RealmIllusionProfile", menuName = "Simulacrum/RealmIllusionProfile")]
public class RealmIllusionProfile : ScriptableObject
{
    public string realmName;
    public IllusionEntry[] illusions;
}

[Serializable]
public class IllusionEntry
{
    public MoodAxisType axisType;
    public float thresholdValue = 80f;
    public bool oneShot = true;
    public GameObject illusionPrefab;
    public Vector3 spawnOffset;
    public AudioClip illusionAudio;
    public float illusionDuration = 10f;
}
