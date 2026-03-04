using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSceneDirector : MonoBehaviour
{
    [SerializeField] string nextSceneName;

    private void Start()
    {
        InitScene();
    }

    public void SceneReady() {
        GameManager.SceneLoader.SceneReady();
    }

    public void GoToScene() {
        GameManager.Player.ReadUIInfo();
        GameManager.SceneLoader.LoadScene(nextSceneName);
    }
    protected virtual void InitScene() { }

}
