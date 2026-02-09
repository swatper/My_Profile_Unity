using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLightController : TimeSensitiveControllerBase
{
    [SerializeField] Light2D globalLight;
    [Header("시간대별 빛 밝기")]
    [SerializeField] Color32 moringLight = new Color32(255, 255, 255, 0);
    [SerializeField] Color32 dayLight = new Color32(255, 215, 150, 0);
    [SerializeField] Color32 nightLight = new Color32(78, 84, 100, 0);
    [SerializeField] float changeDuration = 0.6f;

    protected override void CheckTime(Define.TimeOfDay timeOfDay) {
        switch (timeOfDay) {
            case Define.TimeOfDay.Morning:
                StartCoroutine(ChangeLightColor(moringLight, 1.0f));
                break;
            case Define.TimeOfDay.Day:
                StartCoroutine(ChangeLightColor(dayLight, 0.8f));
                break;
            case Define.TimeOfDay.Night:
                StartCoroutine(ChangeLightColor(nightLight, 0.4f));
                break;
        }
    }

    IEnumerator ChangeLightColor(Color32 targetColor, float targetIntensity) {
        float elapsedTime = 0.0f;
        Color32 startColor = globalLight.color;
        float startIntensity = globalLight.intensity;

        while (elapsedTime < changeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime * changeDuration;
            globalLight.color = Color.Lerp(startColor, targetColor, t);
            globalLight.intensity = Mathf.Lerp(startIntensity, targetIntensity, t);
            yield return null;
        }

        //오차 보정
        globalLight.color = targetColor;
        globalLight.intensity = targetIntensity;
    }
}
