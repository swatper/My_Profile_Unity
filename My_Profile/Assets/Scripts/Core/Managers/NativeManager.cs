using UnityEngine;

/// <summary>
/// 실행 플랫폼 환경(WebGL, Editor, PC 등)에 따라
///  그에 맞는 네이티브 기능을 사용
/// </summary>
public class NativeManager
{
    public void CopyToClipboard(string text){
#if !UNITY_EDITOR && UNITY_WEBGL
        //🌐 WebGL 빌드 환경일 때 실행
        try
        {
            GameManager.Plugin.CopyWebClipbaord(text);
        }
        catch (System.EntryPointNotFoundException e)
        {
            Debug.LogError("WebGL 클립보드 플러그인을 찾을 수 없습니다: " + e.Message);
        }
#else
        //💻 유니티 에디터나 PC 빌드 환경일 때 실행
        GUIUtility.systemCopyBuffer = text;
#endif
        Debug.Log($"[NativeManager] 복사 완료: {text}");
    }

    public void SetFullScreen(bool IsOn)
    {
        GameManager.Instance.IsFullScreen  = IsOn;
#if UNITY_EDITOR
        //에디터 환경
        Debug.Log("[NativeManager] 에디터에서는 전체 화면 테스트 불가능");

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
#endif
        Screen.fullScreen = IsOn;
    }

    /// <summary>
    /// 외부 환경의 전체 화면 상태 동기화
    /// </summary>
    public void SubscribeScreenListener() {
#if !UNITY_EDITOR && UNITY_WEBGL
        //NativeManager가 PluginManager를 통해 Js 이벤트 리스너 등록
        GameManager.Plugin.SubscribeWebScreenListener();
#else
        Debug.Log("[NativeManager] 에디터/PC 환경이므로 웹 리스너 등록 스킵");
#endif
    }
}
