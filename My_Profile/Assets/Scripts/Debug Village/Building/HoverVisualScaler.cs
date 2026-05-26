using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 마우스 접근에 따른 Sprite 크기 조절
/// </summary>
public class HoverVisualScaler : HoverableObject
{
    [Header("Sprite 게임 오브젝트")]
    [SerializeField] Transform spriteObj;
    [Header("효과 설정")]
    float duration = 0.15f;
    [SerializeField] Vector3 originalScale;
    [SerializeField] Vector3 expandScale;
    private Coroutine scaleCoroutine;

    public override  void OnHoverEnter(PointerEventData eventData){
        if (spriteObj == null) 
            return;

        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);

        scaleCoroutine = StartCoroutine(ChangeScaleRoutine(expandScale));
    }

    public override void OnHoverExit(PointerEventData eventData){
        if (spriteObj == null) 
            return;

        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);

        scaleCoroutine = StartCoroutine(ChangeScaleRoutine(originalScale));

    }

    IEnumerator ChangeScaleRoutine(Vector3 targetScale)
    {
        Vector3 initialScale = spriteObj.localScale;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float percentage = elapsedTime / duration;
            spriteObj.localScale = Vector3.Lerp(initialScale, targetScale, percentage);
            yield return null; 
        }

        //마지막 보정
        spriteObj.localScale = targetScale;
        scaleCoroutine = null;
    }
}
