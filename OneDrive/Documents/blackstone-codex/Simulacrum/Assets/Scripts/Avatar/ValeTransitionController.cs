using UnityEngine;
using System.Collections;

public class ValeTransitionController : MonoBehaviour
{
    public Renderer valeRenderer; // Uses a shader that supports fade.
    public AudioSource transitionSound;
    public float transitionDuration = 1.0f;
    private Material valeMaterial;

    void Start()
    {
        if (valeRenderer != null)
            valeMaterial = valeRenderer.material;
    }

    public void FadeIn()
    {
        StartCoroutine(FadeRoutine(0f, 1f));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeRoutine(1f, 0f));
    }

    IEnumerator FadeRoutine(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        if (transitionSound != null)
            transitionSound.Play();

        while (elapsed < transitionDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / transitionDuration);
            SetAlpha(alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }
        SetAlpha(endAlpha);
    }

    void SetAlpha(float alpha)
    {
        if (valeMaterial != null)
        {
            Color c = valeMaterial.color;
            c.a = alpha;
            valeMaterial.color = c;
        }
    }
}
