using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Managers
    public static GameManager GM_Instance;
    public static GameManager Instance { get { Init(); return GM_Instance; } }

    InputManager M_Input = new InputManager();
    public static InputManager Input { get { return Instance.M_Input; } }
    #endregion

    public TimeUpdater TimeUpdater = new TimeUpdater();
    public static TimeUpdater Clock { get { return Instance.TimeUpdater; } }

    public PlayerController pController;
    public static PlayerController Player { get { return Instance.pController; } }

    [Header("시간 관련 데이터 및 이벤트")]
    [Tooltip("확인 및 수동 조작용")]
    [SerializeField] public Define.TimeOfDay curTOD;
    [Header("정각 알리미")]
    public Action OnHourChanged;
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
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            pController = playerObject.GetComponent<PlayerController>();
        }
        StartCoroutine(UpdateTimePerSec());
    }

    private void Update() {
        Input.KeyEvent();
    }

    private void CheckTime() {
        /*
        //시간대 알림
        int curHour = int.Parse(GameManager.Clock.localHour);
        //저녁/밤: 18시부터 4시
        if (curHour >= 18 || curHour < 5)
            curTOD = Define.TimeOfDay.Night;
        //아침: 5시부터 11시
        else if (curHour >= 5 && curHour < 12)
            curTOD = Define.TimeOfDay.Morning;
        //점심/낮: 12시부터 18시
        else
            curTOD = Define.TimeOfDay.Day;
        */

        //Debug.Log($"{curTOD}");
        OnTimeOfDayChanged?.Invoke(curTOD);

        //정각 알림
        if (Clock.localMinute == "00" && Clock.localSecond == "00"){
            OnHourChanged?.Invoke();
        }
    }

    IEnumerator UpdateTimePerSec() {
        while (true) {
            TimeUpdater.UpdateTime();
            CheckTime();
            yield return new WaitForSeconds(1.0f);
        }
    }
}
