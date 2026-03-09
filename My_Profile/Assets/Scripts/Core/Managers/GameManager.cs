using System;
using System.Collections;
using System.Xml.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Managers
    public static GameManager GM_Instance;
    public static GameManager Instance { get { Init(); return GM_Instance; } }

    InputManager M_Input = new InputManager();
    public static InputManager Input { get { return Instance.M_Input; } }

    ResourceManager M_Resource = new ResourceManager();
    public static ResourceManager Resource { get { return Instance.M_Resource; } }

    LoadingUI M_Scene;
    public static LoadingUI SceneLoader { get { return Instance.M_Scene; } }

    public TimeUpdater TimeUpdater = new TimeUpdater();
    public static TimeUpdater Clock { get { return Instance.TimeUpdater; } }
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
    [SerializeField] public Define.TimeOfDay curTOD;
    [Header("시간대 알리미")]
    public Action<Define.TimeOfDay> OnTimeOfDayChanged;

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
        StartTimer();
        SetupUILoading();
    }

    private void Update() {
        Input.KeyEvent();
    }

    void SetupUILoading() {
        if (M_Scene == null)
        {
            GameObject loadingObj = M_Resource.Instantiate("LoadingUI");
            M_Scene = loadingObj.GetComponent<LoadingUI>();
            DontDestroyOnLoad (loadingObj);
        }
    }

    private void CheckTime() {
        //시간대 알림
        int curHour = Clock.Hour;
        int curMin = Clock.Minute;
        int curSec = Clock.Second;
        

        //TODO: 추후 TimerUpdater로 옮길 예정
        //저녁/밤: 18시부터 4시
        if (curHour >= 18 || curHour < 5)
            curTOD = Define.TimeOfDay.Night;
        //아침: 5시부터 11시
        else if (curHour >= 5 && curHour < 12)
            curTOD = Define.TimeOfDay.Morning;
        //점심/낮: 12시부터 18시
        else
            curTOD = Define.TimeOfDay.Day;
        OnTimeOfDayChanged?.Invoke(curTOD);

    }

    //1초마다 시간 측정
    IEnumerator UpdateTimePerSec() {
        while (true) {
            TimeUpdater.UpdateTime();
            CheckTime();
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void StopTimer() {
        StopCoroutine(timeRoutine);
        curTOD = Define.TimeOfDay.Morning;
        OnTimeOfDayChanged?.Invoke(curTOD);
    }

    public void StartTimer() {
        timeRoutine = StartCoroutine(UpdateTimePerSec());
    }
}
