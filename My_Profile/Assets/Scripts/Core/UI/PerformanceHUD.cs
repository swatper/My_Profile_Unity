using System.Diagnostics;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

public class PerformanceHUD : MonoBehaviour
{
    [Header("HUD Elements")]
    [Tooltip("FPS 측정")]
    [SerializeField] private Text fpsText;
    [Tooltip("메모리 사용량 측정")]
    [SerializeField] private Text memoryText;

    [Header("설정")]
    [SerializeField] private float updateInterval = 0.3f;
    float deltaTime = 0.0f;
    private float lastUpdateTime = 0.0f;
    private Process currentProcess;

    private void Awake(){
        currentProcess = Process.GetCurrentProcess();
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        // UI 텍스트 갱신은 지정한 주기마다 실행 (성능 최적화 및 가비지 방지)
        if (Time.unscaledTime - lastUpdateTime >= updateInterval)
        {
            UpdateFPS();
            UpdateMemory();
            lastUpdateTime = Time.unscaledTime;
        }
    }

    /// <summary>
    /// FPS 및 프레임 타임 갱신
    /// </summary>
    private void UpdateFPS()
    {
        if (fpsText == null) return;

        float fps = 1.0f / deltaTime;
        float frameTimeMs = deltaTime * 1000.0f;

        fpsText.text = string.Format("{0:0.} FPS ({1:0.0} ms)", fps, frameTimeMs);
    }

    /// <summary>
    /// 메모리 사용량 갱신 (Byte -> MB 변환)
    /// </summary>
    private void UpdateMemory()
    {
        if (memoryText == null) 
            return;

        currentProcess.Refresh();
        //2. [유니티 예약] 유니티가 OS로부터 선점해둔 총 가상 공간
        long osReservedMemory = Profiler.GetTotalReservedMemoryLong() / (1024 * 1024);

        //3. [유니티 실사용] 현재 씬에 로드된 에셋들의 순수 물리 용량
        long engineActiveMemory = Profiler.GetTotalAllocatedMemoryLong() / (1024 * 1024);

        //4. [C# 논리 힙] C# 코드 및 가비지가 차지하는 공간
        long monoHeapMemory = Profiler.GetMonoUsedSizeLong() / (1024 * 1024);

        // UI 텍스트 출력
        memoryText.text = string.Format(
            "<color=yellow>OS Reserved (UA): {0} MB</color>\n" +
            "<color=lime>Engine Active (Asset): {1} MB</color>\n" +
            "<color=cyan>C# Mono Heap (Code): {2} MB</color>",
            osReservedMemory, engineActiveMemory, monoHeapMemory
        );
    }
}
