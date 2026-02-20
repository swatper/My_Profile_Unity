using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundIcon : OnOffIconBase
{
    [SerializeField] SoundClipController soundController;

    protected override void AddtionalProcessing()
    {
        if(IsOn)
            soundController.MuteAllSound();
        else
            soundController.UnMuteAllSound();
    }
}
