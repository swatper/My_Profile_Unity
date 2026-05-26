using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using Core.Define;

public class SurvivalPortal : BuildingInteraction
{
    [SerializeField] DebugSceneDirector sceneDirector;
    protected override void OnInteract(KeyEvent keyEvent)
    {
        if (keyEvent == KeyEvent.Enter) {
            if (GameManager.SceneLoader.isLoading) 
                return;
            //sceneDirector.GoToScene();
            sceneDirector.OpenCheckUI();
        }
    }
}
