using UnityEngine;

public class FullScreenIcon :  BaseOnOffIconController
{
    public override void InitStatus(){
        //전체 화면 상태 동기화 등록
        GameManager.Instance.SubscribeScreenSync(SyncFullScreen);
        //NativeMager에게 이벤트 리스너 등록하라고 요청
        GameManager.Native.SubscribeScreenListener();
        IsOn = GameManager.Instance.IsFullScreen;

        base.InitStatus();
    }

    /// <summary>
    /// 웹 -> 유니티 | 초기화 및 ESC를 통한 화면 변경
    /// </summary>
    /// <param name="screenState"></param>
    public void SyncFullScreen(bool screenState){
        IsOn = screenState;
        CheckSprite();
    }

    protected override void ActionalProcessing(){
        GameManager.Native.SetFullScreen(IsOn);
    }
     
    private void OnDestroy(){
        GameManager.Instance.UnsubscribeScreenSync(SyncFullScreen);
    }
}
