using UnityEngine;

public class FullScreenIcon :  OnOffIconBase
{
    public override void InitStatus(){
        GameManager.Native.SubscribeScreenListener();
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
    protected override void ActionalProcessing(){
        GameManager.Native.SetFullScreen(IsOn);
    }
}
