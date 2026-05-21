using UnityEngine;
using Core.Define;

public class BuildingInteraction: KeyHintDisplay
{
    [Header("보여줄 UI 요소")]
    [SerializeField] public GameObject infomaionUI;
    [SerializeField] public InfoUI uiScript;
    [SerializeField] public string buildingName;

    /// <summary>
    /// 정보가 담긴 UI 보여주기
    /// </summary>
    /// <param name="keyEvent"></param>
    protected  override void OnInteract(KeyEvent keyEvent)
    {
        if (keyEvent == KeyEvent.Enter) {
            uiScript.Show();
        }
    }
}
