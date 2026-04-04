using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_MobileButton : OnOffIconBase
{
    [SerializeField] BaseSceneDirector sceneDirector;

    public override void InitStatus()
    {
        IsOn = GameManager.Instance.isMobile;
        base.InitStatus();
    }

    protected override void ActionalProcessing()
    {
        GameManager.Instance.isMobile = IsOn;
        if (IsOn){
            sceneDirector.SetMobileMode();
        }
        else{
            sceneDirector.SetPcMode();
        }
    }
}
