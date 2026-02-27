using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenIcon :  OnOffIconBase
{
    protected override void AddtionalProcessing()
    {
        Screen.fullScreen = IsOn;
    }
}
