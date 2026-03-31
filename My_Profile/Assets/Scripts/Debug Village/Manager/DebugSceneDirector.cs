using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugSceneDirector : BaseSceneDirector
{
    public static DebugSceneDirector Instance { get; private set; }
    bool IsSceneProtected = false;
    [SerializeField] GameObject checkUI;
    [SerializeField] InputField code;
    [SerializeField] Text message;
    public VillageSoundController soundController;

    private void Awake(){
        Instance = this;
    }

    protected override void InitScene()
    {
        base.InitScene();
        GameManager.Player.InitPlayerInVillagel();
        GameManager.Player.StopReadUIInfo();
        SceneReady();
    }

    public override void MuteSound(){
        soundController.MuteAllSound();
    }

    public override void UnMuteSound(){
        soundController.UnMuteAllSound();
    }

//#if UNITY_EDITOR
    /// <summary>
    /// 개발 완료 후 되돌릴 예정
    /// </summary>
    public void OpenCheckUI() {
        if (!IsSceneProtected) {
            GoToScene();
        }
        else{
            GameManager.Player.ReadUIInfo();
            checkUI.SetActive(true);
            code.text = "";
            message.text = "Insert Code:";
            code.ActivateInputField();
        }
    }

    public void CheckCode() {
        if (code.text == "I'm not a robot") {
            checkUI.SetActive(false);
            GoToScene();
        }
        else{
            message.text = "Wrong Code";
        }
    }

    public void CloseCheckUI() {
        GameManager.Player.StopReadUIInfo();
        checkUI.SetActive(false);
    }
//#endif
}
