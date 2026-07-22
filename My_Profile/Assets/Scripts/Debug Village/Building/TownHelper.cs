using System;
using Core.Define;
using UnityEngine;

public class TownHelper : MonoBehaviour
{
    [Header("건물")]
    public HoverVisualScalerWorld[] buildings;
    //[Header("UI")
    //public  GameObject helpUI;
    [Tooltip("설명 필요 상태 여부")]
    [SerializeField] bool needHelp = false;
    void Start(){
        GameManager.Input.SubscribeKeyEvent(ShowTownInfo);
    }

    /// <summary>
    /// 모든 건물 호버링 (확대 및 정보 표시)
    /// </summary>
    void ShowTownInfo(KeyEvent keyEvent){
        if (keyEvent != KeyEvent.Help)
            return;

        /*
        if (keyEvent != KeyEvent.Help) {
            if (!needHelp)
                return;
            needHelp = false;
        }*/

        needHelp = !needHelp;
        for (int building = 0; building < buildings.Length; building++){
            buildings[building].SetHoverState(needHelp);
        }

       //helpUI.SetActive(needHelp);
    }

    private void OnDestroy(){
        GameManager.Input.RemoveSubscribe(ShowTownInfo);
    }
}
