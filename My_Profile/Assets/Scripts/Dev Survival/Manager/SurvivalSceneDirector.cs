using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class SurvivalSceneDirector : BaseSceneDirector
{
    public static SurvivalSceneDirector Instance { get; private set; }
    public PoolManager poolManager;
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        Instance = this;
        GameManager.Player.SubscribeLevelUp(OnLevelUp);

    }
    protected override void InitScene()
    {
        virtualCamera.Follow = GameManager.Player.transform;
        poolManager = FindObjectOfType<PoolManager>();
        GameManager.Player.InitPlayerInSurvival();
        GameManager.SceneLoader.SceneReady();
    }

    void OnLevelUp() { 
    }

    private void OnDestroy()
    {
        GameManager.Player.UnsubscribeLevelUp(OnLevelUp);
    }
}
