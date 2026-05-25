using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 마우스 호버 상호작용
/// </summary>
public interface IHoverable : IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// 마우스가 오브젝트 위에 올라왔을 땐
    /// </summary>
    void OnHoverEnter();

    /// <summary>
    /// 마우스가 오브젝트 밖으로 나갔을 때
    /// </summary>
    void OnHoverExit();
}
