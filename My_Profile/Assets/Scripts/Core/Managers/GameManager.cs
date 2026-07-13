using Core.Define;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Windows;

public class GameManager : MonoBehaviour
{
    #region Managers
    public static GameManager GM_Instance;
    public static GameManager Instance { get { Init(); return GM_Instance; } }

    InputManager M_Input;
    public static InputManager Input { get { return Instance.M_Input; } }

    ResourceManager M_Resource = new ResourceManager();
    public static ResourceManager Resource { get { return Instance.M_Resource; } }

    LoadingUI M_Scene;
    public static LoadingUI SceneLoader { get { return Instance.M_Scene; } }

    public TimeUpdater TimeUpdater = new TimeUpdater();
    public static TimeUpdater Clock { get { return Instance.TimeUpdater; } }

    GameObject PerformanceHUD;

    public PluginManager M_Plugin= new PluginManager();
    public static PluginManager Plugin { get { return Instance.M_Plugin; } }

    public DataManager M_Data = new DataManager();
    public static DataManager Data { get { return Instance.M_Data; } }

    public NativeManager M_Native = new NativeManager();
    public static NativeManager Native { get { return Instance.M_Native; } }
    #endregion

    public PlayerController pController;
    public static PlayerController Player {
        get
        {
            if (Instance.pController == null)
            {
                GameObject go = GameObject.FindWithTag("Player");  // Name보다는 Tag가 성능상 유리
                if (go != null) Instance.pController = go.GetComponent<PlayerController>();
            }
            return Instance.pController;
        }
    }

    [Header("시간 관련 데이터 및 이벤트")]
    Coroutine timeRoutine;
    [Tooltip("확인 및 수동 조작용")]
    [SerializeField] public TimeOfDay testingTOD;
    [SerializeField] bool isTesting = false;
    [Header("HUD 상태")]
    bool isDisplaying = false;
    [Header("버튼 상태")]
    public bool IsMute = false;
    public bool IsFullScreen = false;
    public bool isMobile = false;
    [Header("전체 화면 상태")]
    public Action<bool> OnFullScreenChangeEvt;

    static void Init()
    {
        if (GM_Instance == null)
        {
            GameObject gmObject = GameObject.Find("GameManager");
            if (gmObject == null)
            {
                gmObject = new GameObject { name = "GameManager" };
                gmObject.AddComponent<GameManager>();
            }
            GM_Instance = gmObject.GetComponent<GameManager>();
            DontDestroyOnLoad(gmObject);
        }
    }

    private void Awake()
    {
        M_Input = new InputManager();
        //프레임 설정 (제한 없음-> 하드웨어와 브라우저 설정이 허락하는 최대치)
        Application.targetFrameRate = -1;

        SetupUILoading();
        InitPerformanceHUD();
    }

    private void Update() {
        Input.OnKeyEvent();
    }

    private void Start()
    {
        //시간 초기화 및 측정 시작
        InitTime();
        StartTimer();

        Input.SubscribeKeyEvent(SetPerformanceHUD);
    }

    #region 전체화면 연동 관련
    public void SubscribeScreenSync(Action<bool> action){
        OnFullScreenChangeEvt += action;
    }

    public void UnsubscribeScreenSync(Action<bool> action){
        OnFullScreenChangeEvt -= action;
    }

    /// <summary>
    /// 외부(웹) -> 인게임 오브젝트 연동 (사실상 아이콘 상태  연동)
    /// </summary>
    /// <param name="isFullscreen">1: 전체화면 0: 축소</param>
    public void SyncWebFullScreen(int isFullscreen){
        IsFullScreen = isFullscreen == 1;
        OnFullScreenChangeEvt?.Invoke(IsFullScreen);
    }

    #endregion

    void InitPerformanceHUD()
    {
        if (PerformanceHUD == null) {
            PerformanceHUD = M_Resource.Instantiate("PerformanceHUD", null, false);
            DontDestroyOnLoad(PerformanceHUD);
        }
    }

    void SetPerformanceHUD(KeyEvent keyEvent) {
        if (keyEvent == KeyEvent.Debug) {
            isDisplaying = !isDisplaying;
            PerformanceHUD.SetActive(isDisplaying);
        }
    }

    void SetupUILoading() {
        if (M_Scene == null)
        {
            GameObject loadingObj = M_Resource.Instantiate("LoadingUI");
            M_Scene = loadingObj.GetComponent<LoadingUI>();
            DontDestroyOnLoad(loadingObj);
        }
    }

    //1초마다 시간 측정
    IEnumerator UpdateTimePerSec() {
        while (true)
        {
            if (isTesting)
                Clock.OnTimeOfDayChanged.Invoke(testingTOD);
            else
                TimeUpdater.UpdateTime();
            yield return new WaitForSeconds(1.0f);
        }
    }

    void InitTime()
    {
        Clock.InitClock();
    }

    public void StopTimer() {
        StopCoroutine(timeRoutine);
        testingTOD = TimeOfDay.Morning;
        Clock.OnTimeOfDayChanged.Invoke(testingTOD);
    }

    public void StartTimer() {
        timeRoutine = StartCoroutine(UpdateTimePerSec());
    }
}
