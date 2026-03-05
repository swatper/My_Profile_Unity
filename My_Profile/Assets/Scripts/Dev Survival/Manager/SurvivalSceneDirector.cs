using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class SurvivalSceneDirector : BaseSceneDirector
{
    public static SurvivalSceneDirector Instance { get; private set; }
    [SerializeField] public PoolManager poolManager;
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    protected override void InitScene()
    {
        virtualCamera.Follow = GameManager.Player.transform;
        poolManager = FindObjectOfType<PoolManager>();
        GameManager.Player.InitPlayerInSurvival();
        GameManager.SceneLoader.SceneReady();
    }
}
