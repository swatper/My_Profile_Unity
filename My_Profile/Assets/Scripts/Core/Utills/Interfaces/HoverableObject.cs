using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 마우스 호버 상호작용
/// </summary>
public abstract class HoverableObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
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
