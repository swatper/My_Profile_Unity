using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lamp : TimeSensitiveControllerBase
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Light2D lampLight;
    [SerializeField] Sprite lampOff;
    [SerializeField] Sprite lampOn;
    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lampLight = GetComponentInChildren<Light2D>();
    }

    protected override void CheckTime(Define.TimeOfDay timeOfDay) {
        if (timeOfDay == Define.TimeOfDay.Night) {
            spriteRenderer.sprite = lampOn;
            lampLight.enabled = true;
        }
        else{
            spriteRenderer.sprite = lampOff;
            lampLight.enabled = false;
        }
    }
}
