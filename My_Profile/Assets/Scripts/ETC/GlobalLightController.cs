using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLightController : TimeSensitiveControllerBase
{
    [SerializeField] Light2D golbalLight;
    [Header("½Ã°£´ëº° ºû ¹à±â")]
    [SerializeField] Color moringLight = Color.white;
    [SerializeField] Color dayLight;
    [SerializeField] Color nightLight; 

    protected override void CheckTime(Define.TimeOfDay timeOfDay) {
        switch (timeOfDay) {
            case Define.TimeOfDay.Morning:
                golbalLight.color = moringLight;
                break;
            case Define.TimeOfDay.Day:
                golbalLight.color = dayLight;
                break;
            case Define.TimeOfDay.Night:
                golbalLight.color = nightLight;
                break;
        }
    }
}
