using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerformanceHUD : MonoBehaviour
{
    [Header("HUD Elements")]
    [SerializeField] private Text fpsText;

    float deltaTime = 0.0f;
    void Update()
    {
        // 이전 프레임과의 시간 차이를 누적하고 평균을 냄
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = string.Format("{0:0.} FPS", fps);
    }
}
