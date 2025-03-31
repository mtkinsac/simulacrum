using UnityEngine;
using System.Collections;

public class ValeTransitionController : MonoBehaviour
{
    public UnityEngine.UI.Image fadeImage; // Now targets UI Image instead of Renderer
    public AudioSource transitionSound;
    public float transitionDuration = 1.0f;

    void Start()
    {
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = 0f;
            fadeImage.color = c;
        }
    }

    [ContextMenu("Fade In")]
    public void FadeIn()
    {
        StartCoroutine(FadeRoutine(0f, 1f));
    }

    [ContextMenu("Fade Out")]
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
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = alpha;
            fadeImage.color = c;
        }
    }
}
