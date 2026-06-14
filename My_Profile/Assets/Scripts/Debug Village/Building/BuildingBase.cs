using UnityEngine;

/// <summary>
/// 구버전 빌딩 스크립트
/// </summary>
public abstract  class BuildingBase : MonoBehaviour
{
    [SerializeField] public GameObject infomaionUI;
    [SerializeField] public InfoUI uiScript;
    [SerializeField] public string buildingName;

    /// <summary>
    /// 충돌 감지로 UI 보여줌
    /// </summary>
    /// <param name="collision"></param>
    public abstract void OnTriggerEnter2D(Collider2D collision);
}
