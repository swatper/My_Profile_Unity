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

    private void OnEnable()
    {
        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);

        //UI가 켜지는 순간 마우스가 이미 이 슬롯 위에 올려져 있는지 체크
        if (EventSystem.current != null)
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;

            var results = new System.Collections.Generic.List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, results);

            foreach (var result in results)
            {
                if (result.gameObject == null) continue;

                // 레이캐스트에 걸린 오브젝트가 나 자신이거나 내 자식(텍스트, 아이콘 등)이라면
                if (result.gameObject == this.gameObject || result.gameObject.transform.IsChildOf(this.transform))
                {
                    // 마우스가 올려진 채로 켜진 것이므로 즉시 확장 연출 시작
                    scaleCoroutine = StartCoroutine(ChangeScaleRoutine(expandScale, true));
                    break;
                }
            }
        }
    }

    //UI 비활성화 시 강제 축소
    private void OnDisable()
    {
        //실행 중인 코루틴 안전하게 제거
        if (scaleCoroutine != null){
            StopCoroutine(scaleCoroutine);
            scaleCoroutine = null;
        }

        //크기를 원래 상태로 강제 초기화 (다음 번에 켜졌을 때 연출이 꼬이지 않도록)
        if (targetObj != null){
            targetObj.localScale = originalScale;
        }
    }

    public override void OnHoverEnter(PointerEventData eventData)
    {
        if (targetObj == null)
            return;

        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);
        scaleCoroutine = StartCoroutine(ChangeScaleRoutine(expandScale, true));
    }

    public override void OnHoverExit(PointerEventData eventData)
    {
        if (targetObj == null)
            return;

        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);
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
    }
}
