using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 마우스 호버 상호작용
/// </summary>
public abstract class HoverableObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("시각적 연출 대상")]
    [Tooltip("대상이 없으면 기본적으로 스크립트를 갖고 있는 대상")]
    public Transform targetObj;

    void Reset(){
        if(targetObj == null)
            targetObj = transform;
    }

    public void OnPointerEnter(PointerEventData eventData){
        OnHoverEnter(eventData);
    }

    public void OnPointerExit(PointerEventData eventData){
        OnHoverExit(eventData);
    }

    /// <summary>
    /// 마우스가 오브젝트 위에 올라왔을 땐
    /// </summary>
    public abstract void OnHoverEnter(PointerEventData mouseData);

    /// <summary>
    /// 마우스가 오브젝트 밖으로 나갔을 때
    /// </summary>
    public abstract void OnHoverExit(PointerEventData mouseData);
}
