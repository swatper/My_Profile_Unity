using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Define;

[System.Serializable]
public class TimeUpdater
{
    [Header("시간 알리미")]
    public Action<string> OnTimeUpdated;
    [Header("정각 알리미")]
    public Action OnHourChanged;
    [Header("시간대 알리미")]
    public TimeOfDay curTOD;
    public Action<TimeOfDay> OnTimeOfDayChanged;

    [Tooltip("로컬 시간 데이터")]
    public int Year { get; private set; }
    public int Month { get; private set; }
    public int Day { get; private set; }
    public int Hour { get; private set; }
    public int Minute { get; private set; }
    public int Second { get; private set; }
    private bool showColon = true;
    string timeColon;
    string timeText;

    void SyncRealTime()
    {
        DateTime now = DateTime.Now;
        Hour = now.Hour;
        Minute = now.Minute;
        Second = now.Second;

        timeColon = showColon ?  ":" : " ";
        timeText = $"{Hour:D2}{timeColon}{Minute:D2}";
        showColon = !showColon;

        OnTimeUpdated?.Invoke(timeText);
    }

    /// <summary>
    /// 최초 실행
    /// </summary>
    public void InitClock()
    {
        showColon = false;
        SyncRealTime();
        CheckTOD();
    }

    /// <summary>
    /// 시간대 측정
    /// </summary>
    void CheckTOD()
    {
        //저녁/밤: 18시부터 4시
        if (Hour >= 18 || Hour < 5)
            curTOD = TimeOfDay.Night;
        //아침: 5시부터 11시
        else if (Hour >= 5 && Hour < 12)
            curTOD = TimeOfDay.Morning;
        //점심/낮: 12시부터 18시
        else
            curTOD = TimeOfDay.Day;

        OnTimeOfDayChanged?.Invoke(curTOD);
    }

    /// <summary>
    /// 실시간 시간 처리
    /// </summary>
    public void UpdateTime() {
        if (Second == DateTime.Now.Second) return;

        SyncRealTime();

        int prevMinute = Minute;

        if (prevMinute != Hour) {
            CheckTOD();
            //정각 알림
            if (Hour == 0 && Minute == 0) {
                OnHourChanged?.Invoke();
            }
        }
    }

    /// <summary>
    /// 초 단위 알람
    /// </summary>
    /// <param name="method"></param>
    public void SubscribeOnRealTime(Action<string> method){
        OnTimeUpdated += method;
    }

    public void UnsubscribeOnRealTime(Action<string> method){
        OnTimeUpdated -= method;
    }
    /// <summary>
    /// 시간 단위 알람
    /// </summary>
    /// <param name="method"></param>
    public void SubscribeHourlyAlarm(Action method) {
        OnHourChanged += method;
    }

    public void UnsubscribeHourlyAlarm(Action method) {
        OnHourChanged -= method;
    }

    /// <summary>
    /// 시간대 알람
    /// </summary>
    /// <param name="method"></param>
    public void SubscribeTimeOfDayAlarm(Action<TimeOfDay> method)
    {
        OnTimeOfDayChanged += method;
    }

    public void UnsubscribeTimeOfDayAlarm(Action<TimeOfDay> method)
    {
        OnTimeOfDayChanged -= method;
    }
}
