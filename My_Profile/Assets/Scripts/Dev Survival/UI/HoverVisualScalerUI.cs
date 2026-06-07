using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class HoverVisualScalerUI : HoverableObject
{
    /// <summary>
    /// UI 오브젝트 마우스 호버에 따른 Sprite 크기 조절
    /// </summary>
    [Header("연출 시간")]
    [SerializeField] protected float duration = 0.15f;
    protected Coroutine scaleCoroutine;

    [Header("효과 설정")]
    [SerializeField] Vector3 originalScale;
    [SerializeField] Vector3 expandScale;

    private void OnEnable(){
        StopScaleCoroutine();
    }

    //UI 비활성화 시 강제 축소
    private void OnDisable()
    {
        StopScaleCoroutine();

        //크기를 원래 상태로 강제 초기화 (다음 번에 켜졌을 때 연출이 꼬이지 않도록)
        if (targetObj != null)
            targetObj.localScale = originalScale;
    }

    public override void OnHoverEnter(PointerEventData eventData)
    {
        if (targetObj == null)
            return;

        StopScaleCoroutine();
        scaleCoroutine = StartCoroutine(ChangeScaleRoutine(expandScale, true));
    }

    public override void OnHoverExit(PointerEventData eventData)
    {
        if (targetObj == null)
            return;

        StopScaleCoroutine();
        scaleCoroutine = StartCoroutine(ChangeScaleRoutine(originalScale, false));
    }

    /// <summary>
    /// 실행 중인 코루틴 제거
    /// </summary>
    void StopScaleCoroutine() {
        if (scaleCoroutine != null){
            StopCoroutine(scaleCoroutine);
            scaleCoroutine = null;
        }
    }

    /// <summary>
    /// 크기 변화 연출
    /// </summary>
    /// <param name="targetScale"></param>
    /// <param name="isEnter"></param>
    /// <returns></returns>
    IEnumerator ChangeScaleRoutine(Vector3 targetScale, bool isEnter)
    {
        Vector3 initialScale = targetObj.localScale;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float percentage = elapsedTime / duration;
            targetObj.localScale = Vector3.Lerp(initialScale, targetScale, percentage);
            yield return null;
        }

        //마지막 보정
        targetObj.localScale = targetScale;
        scaleCoroutine = null;
    }
}
