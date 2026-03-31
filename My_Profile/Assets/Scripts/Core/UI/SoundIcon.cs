using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundIcon : OnOffIconBase
{
    [SerializeField] BaseSceneDirector sceneDirector;

    public override void InitStatus()
    {
        IsOn = GameManager.Instance.IsMute;
        base.InitStatus();
    }

    protected override void ActionalProcessing()
    {
        GameManager.Instance.IsMute = IsOn;
        if(IsOn)
            sceneDirector.MuteSound();
        else
            sceneDirector.UnMuteSound();
    }
}
