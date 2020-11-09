using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxFade : MonoBehaviour
{
    [SerializeField] float exposureStart = 1;
    [SerializeField] float exposureEnd = 0;
    [SerializeField] float fadeTiming = 0.5f;

    public void FadeDown()
    {
        StartCoroutine(Fade(exposureStart, exposureEnd));
    }

    public void FadeUp()
    {
        StartCoroutine(Fade(exposureEnd, exposureStart));
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeTiming)
        {
            elapsedTime += Time.deltaTime;
            float currentAlpha = Mathf.Lerp(startAlpha, endAlpha, Mathf.Clamp01(elapsedTime / fadeTiming));
            RenderSettings.skybox.SetFloat("_Exposure", currentAlpha);
            yield return new WaitForEndOfFrame();
        }
    }

}
