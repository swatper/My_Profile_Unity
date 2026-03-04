using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SurvivalPortal : KeyHintDisplay
{
    [SerializeField] DebugSceneDirector sceneDirector;
    protected override void OnInteract(Define.KeyEvent keyEvent)
    {
        if (keyEvent == Define.KeyEvent.Enter) {
            if (GameManager.SceneLoader.isLoading || !isEnter) return;
            sceneDirector.GoToScene();
        }
    }
}
