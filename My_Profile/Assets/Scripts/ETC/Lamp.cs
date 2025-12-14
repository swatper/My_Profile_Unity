using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite lampOff;
    [SerializeField] Sprite lampOn;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameManager.Clock.SubscribeOnTimeAlarm(CheckTime);
        CheckTime();
    }

    void CheckTime() {
        int curTime = int.Parse(GameManager.Clock.localHour);

        if (curTime <= 5 || curTime >= 18) 
            spriteRenderer.sprite = lampOn;
        else
            spriteRenderer.sprite = lampOff;
    }
}
