using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// 외부 라이브러리 연결
/// </summary>
public class PluginManager
{
#if !UNITY_EDITOR && UNITY_WEBGL   //클립보드
    [DllImport("__Internal")] private static extern void CopyTextToClipboardWebGL(string text);
    //전체화면
    [DllImport("__Internal")] private static extern void SetFullscreenWebGL(bool isFullscreen);
    //전체화면 감지
    [DllImport("__Internal")] private static extern void RegisterFullscreenListener();

    public void CopyWebClipbaord(string copyText) => CopyTextToClipboardWebGL(copyText);

    public void FullScreenWebGL(bool isFullScreen) => SetFullscreenWebGL(isFullScreen);

    public void SubscribeWebScreenListener() => RegisterFullscreenListener();
#endif
}