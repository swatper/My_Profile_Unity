using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Core.Define;

/// <summary>
/// 시간에 따른 2D 및 처리 (기본은 켜고 끄기)
/// </summary>
public class BaseLightContoller : TimeSensitiveControllerBase
{
    [SerializeField] protected Light2D[] light2D;
    protected override void CheckTime(TimeOfDay timeOfDay)
    {
        if (timeOfDay == TimeOfDay.Night)
            LightOn();
        else
            LightOff();
    }

    public void LightOff() {
        for (int i = 0; i < light2D.Length; i++){
            light2D[i].enabled = false;
        }
    }
    private void LightOn(){
        for (int i = 0; i < light2D.Length; i++){
            light2D[i].enabled = true;
        }
    }
}
