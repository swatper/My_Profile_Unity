using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSceneDirector : MonoBehaviour
{
    [SerializeField] string nextSceneName;
    [SerializeField] OnOffIconBase[] icons;
    public GameObject mobileKeyBoard;

    private void Start(){
        InitScene();
    }

    public void SceneReady() {
        GameManager.SceneLoader.SceneReady();
    }

    public virtual void GoToScene() {
        GameManager.Player.ReadUIInfo();
        GameManager.SceneLoader.LoadScene(nextSceneName);
    }

    protected virtual void InitScene() {
        for (int i = 0; i < icons.Length; i++) {
            icons[i].InitStatus();
        }
    }

    public virtual void MuteSound() { }
    public virtual void UnMuteSound() { }

    public virtual void SetPcMode() {
        mobileKeyBoard.SetActive(false);
    }

    public virtual void SetMobileMode() {
        mobileKeyBoard.SetActive(true);
    }
}
