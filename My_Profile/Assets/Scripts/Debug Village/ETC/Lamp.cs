using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Core.Define;

public class Lamp : BaseLightContoller
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite lampOff;
    [SerializeField] Sprite lampOn;
    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void CheckTime(TimeOfDay timeOfDay) {
        base.CheckTime(timeOfDay);

        if (timeOfDay == TimeOfDay.Night) {
            spriteRenderer.sprite = lampOn;
        }
        else{
            spriteRenderer.sprite = lampOff;
        }
    }
}
