using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 월드 오브젝트(필드 객체) 마우스 호버에 따른 Sprite 크기 조절
/// (주의: UI Canvas 레이어가 아닌 World Space 오브젝트 전용
/// </summary>
public class HoverVisualScalerWorld: HoverableObject
{
    [Header("연출 시간")]
    [SerializeField] protected float duration = 0.15f;
    protected Coroutine scaleCoroutine;
    [Header("효과 설정")]
    [SerializeField] Vector3 originalScale;
    [SerializeField] Vector3 expandScale;
    [SerializeField] Vector3 cursorPos; 
    [Header("역할 설명")]
    [SerializeField] GameObject explain;

    public override  void OnHoverEnter(PointerEventData eventData){
        if (targetObj == null) 
            return;

        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);
        cursorPos = eventData.position;
        scaleCoroutine = StartCoroutine(ChangeScaleRoutine(expandScale, true));
    }

    public override void OnHoverExit(PointerEventData eventData){
        if (targetObj == null) 
            return;

        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);
        cursorPos = Vector3.zero;
        scaleCoroutine = StartCoroutine(ChangeScaleRoutine(originalScale, false));

    }

    IEnumerator ChangeScaleRoutine(Vector3 targetScale, bool isEnter)
    {
        Vector3 initialScale = targetObj.localScale;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float percentage = elapsedTime / duration;
            targetObj.localScale = Vector3.Lerp(initialScale, targetScale, percentage);
            yield return null; 
        }

        //마지막 보정
        targetObj.localScale = targetScale;
        scaleCoroutine = null;
        explain.SetActive(isEnter);
        //explain.transform.position = cursorPos;
    }
}
