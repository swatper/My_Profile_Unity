using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalSceneDirector : BaseSceneDirector
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    protected override void InitScene()
    {
        virtualCamera.Follow = GameManager.Player.transform;
        GameManager.Player.InitPlayerPositionInSurvival();
        GameManager.SceneLoader.SceneReady();
    }
}
