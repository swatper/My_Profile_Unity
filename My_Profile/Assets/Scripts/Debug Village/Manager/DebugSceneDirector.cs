using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSceneDirector : BaseSceneDirector
{
    protected override void InitScene()
    {
        GameManager.Player.InitPlayerPositionInVillagel();
        GameManager.Player.StopReadUIInfo();
        SceneReady();
    }
}
