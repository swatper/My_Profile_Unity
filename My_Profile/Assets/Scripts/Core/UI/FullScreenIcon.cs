using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenIcon :  OnOffIconBase
{
    public override void InitStatus(){
        IsOn = GameManager.Instance.IsFullScreen;
        base.InitStatus();
    }

    protected override void ActionalProcessing()
    {
        GameManager.Instance.IsFullScreen = IsOn;

        Screen.fullScreen = IsOn;   
    }
}
