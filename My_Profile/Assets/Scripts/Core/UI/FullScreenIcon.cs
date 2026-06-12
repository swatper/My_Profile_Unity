using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenIcon :  OnOffIconBase
{
    public override void InitStatus(){
/*
#if UNITY_WEBGL && !UNITY_EDITOR
        GameManager.Plugin.SubscribeWebScreenListener();
#else
        IsOn = GameManager.Instance.IsFullScreen;
#endif
*/
        IsOn = GameManager.Instance.IsFullScreen;

        base.InitStatus();
    }

    //웹 -> 유니티
        //초기화 및 ESC를 통한 화면 변경
    public void SyncFullScreen(int screenState){
        IsOn = screenState == 1;
        GameManager.Instance.IsFullScreen = IsOn;
        CheckSprite();
    }

    //유니티(버튼) -> 웹
    protected override void ActionalProcessing()
    {
        GameManager.Instance.IsFullScreen = IsOn;
#if UNITY_EDITOR
        //에디터 환경
        Debug.Log("에디터에서는 테스트 불가능");

        /* #elif UNITY_WEBGL
                //WebGL 환경
                try{
                    GameManager.Plugin.FullScreenWebGL(IsOn);
                }
                catch (System.EntryPointNotFoundException e){
                    Debug.LogError("전체 화면 실패: " + e.Message);
                    IsOn = !IsOn;
                    GameManager.Instance.IsFullScreen = IsOn;
                    CheckSprite(true);
                }
         */
#else
        //나머지 환경
        Debug.Log("화면 크기 전환");
        Screen.fullScreen = IsOn;
#endif
    }
}
