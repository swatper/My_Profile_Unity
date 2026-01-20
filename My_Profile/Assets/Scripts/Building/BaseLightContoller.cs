using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BaseLightContoller : TimeSensitiveControllerBase
{
    [SerializeField] protected Light2D[] light2D;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void CheckTime(Define.TimeOfDay timeOfDay)
    {
        if (timeOfDay == Define.TimeOfDay.Night)
            LightOn();
        else
            LightOff();
    }

    private void LightOff() {
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
