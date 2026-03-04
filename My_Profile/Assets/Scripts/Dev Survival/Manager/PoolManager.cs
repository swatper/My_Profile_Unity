using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] BaseSceneDirector  sceneDirector;
    [Header("Scene Situation Board")]
    [SerializeField] int maxUnit;
    [SerializeField] int curUnit;

    [SerializeField] Queue<BaseMonsterController> readyPool = new Queue<BaseMonsterController>();

    private void Awake()
    {
        PreParePool();
    }

    public void PreParePool() {
        sceneDirector.SceneReady();
    }
}
