using UnityEngine;
using System.Collections.Generic;

public class RealmIllusionController : MonoBehaviour
{
    public string realmName;
    public RealmIllusionProfile illusionProfile;

    private Dictionary<int, bool> triggeredIllusions = new Dictionary<int, bool>();

    void Start()
    {
        if (illusionProfile != null && illusionProfile.illusions != null)
        {
            for (int i = 0; i < illusionProfile.illusions.Length; i++)
            {
                triggeredIllusions[i] = false;
            }
        }
    }

    void Update()
    {
        if (MemoryMoodManager.Instance == null || illusionProfile == null || !illusionProfile.realmName.Equals(realmName))
            return;

        for (int i = 0; i < illusionProfile.illusions.Length; i++)
        {
            IllusionEntry entry = illusionProfile.illusions[i];
            if (triggeredIllusions[i] && entry.oneShot)
                continue;

            float moodValue = GetAxisValue(entry.axisType);
            if (moodValue >= entry.thresholdValue)
            {
                if (!triggeredIllusions[i])
                {
                    StartCoroutine(TriggerIllusion(entry, i));
                }
            }
        }
    }

    private float GetAxisValue(MoodAxisType axisType)
    {
        return axisType switch
        {
            MoodAxisType.Sadness => MemoryMoodManager.Instance.sadness,
            MoodAxisType.Joy => MemoryMoodManager.Instance.joy,
            MoodAxisType.Awe => MemoryMoodManager.Instance.awe,
            MoodAxisType.Curiosity => MemoryMoodManager.Instance.curiosity,
            _ => 0f,
        };
    }

    private System.Collections.IEnumerator TriggerIllusion(IllusionEntry entry, int index)
    {
        triggeredIllusions[index] = true;
        Debug.Log($"RealmIllusionController: Triggering illusion {entry.axisType} threshold: {entry.thresholdValue}");

        GameObject illusionObj = null;
        if (entry.illusionPrefab != null)
        {
            Vector3 spawnPos = transform.position + entry.spawnOffset;
            illusionObj = Instantiate(entry.illusionPrefab, spawnPos, Quaternion.identity);
        }

        if (entry.illusionAudio != null)
        {
            AudioSource.PlayClipAtPoint(entry.illusionAudio, transform.position);
        }

        yield return new WaitForSeconds(entry.illusionDuration);

        if (illusionObj != null)
        {
            Destroy(illusionObj);
        }
    }
}
