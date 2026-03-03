using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static UnityEditor.PlayerSettings;

public class TimeUpdater
{
    [Header("시간 알리미")]
    public Action<string> OnTimeUpdated;
    [Header("정각 알리미")]
    public Action OnHourChanged;
    //[Header("시간대 알리미")]
    //public Action<Define.TimeOfDay> OnTimeOfDayChanged;

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

    public void UpdateTime() {
        DateTime now = DateTime.Now;

        if (Second == now.Second) return;

        int prevMinute = Minute;
        Hour = now.Hour;
        Minute = now.Minute;
        Second = now.Second;
        Second = now.Second;

        showColon = !showColon;
        timeColon = showColon ? ":" : " ";
        timeText = $"{Hour:D2}{timeColon}{Minute:D2}";

        OnTimeUpdated?.Invoke(timeText);
        //정각 알림
        if (Hour == 0 && Minute == 0)
            OnHourChanged?.Invoke();
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

    public void SubscribeHourlyAlarm(Action method) {
        OnHourChanged += method;
    }

    public void UnsubscribeHourlyAlarm(Action method) {
        OnHourChanged -= method;
    }
}
