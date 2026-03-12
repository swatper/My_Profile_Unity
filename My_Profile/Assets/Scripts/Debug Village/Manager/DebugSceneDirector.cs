using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugSceneDirector : BaseSceneDirector
{
    [SerializeField] bool IsSceneProtected = false;
    [SerializeField] GameObject checkUI;
    [SerializeField] InputField code;
    [SerializeField] Text message;
    protected override void InitScene()
    {
        GameManager.Player.InitPlayerInVillagel();
        GameManager.Player.StopReadUIInfo();
        SceneReady();
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
            message.text = "코드를 입력하세요.";
        }
    }

    public void CheckCode() {
        if (code.text == "개발자가 아닙니다.") {
            checkUI.SetActive(false);
            GoToScene();
        }
        else{
            message.text = "잘못된 코드입니다.";
        }
    }

    public void CloseCheckUI() {
        GameManager.Player.StopReadUIInfo();
        checkUI.SetActive(false);
    }
//#endif
}
