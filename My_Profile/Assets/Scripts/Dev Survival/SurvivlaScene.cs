using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivlaScene : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    private void Awake()
    {
        virtualCamera.Follow = GameManager.Player.transform;
        GameManager.Player.InitPlayerPositionInSurvival();
        GameManager.SceneLoader.SceneReady();
    }

    public void GoToDebugVillage() {
        GameManager.SceneLoader.LoadScene("Debug Village");
    }
}
