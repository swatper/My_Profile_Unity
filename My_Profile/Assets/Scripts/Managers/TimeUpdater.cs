using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class TimeUpdater
{
    [Header("시간 알리미")]
    public Action<string> OnTimeUpdated;
    [Header("정각 알리미")]
    public Action OnHourChanged;

    [Header("로컬 시간 데이터")]
    public string localYear;
    public string localMonth;
    public string localDay;
    public string localHour;
    public string localMinute;
    public string localSecond;

    public void UpdateTime() {
        DateTime dateTime = DateTime.Now;
        localYear = dateTime.Year.ToString("D4");
        localMonth = dateTime.Month.ToString("D2");
        localDay = dateTime.Day.ToString("D2");
        localHour = dateTime.Hour.ToString("D2");
        localMinute = dateTime.Minute.ToString("D2");
        localSecond = dateTime.Second.ToString("D2");
        Debug.Log($"{localHour}:{localMinute}:{localSecond}");

        //정각 알림
        if (localMinute == "00" && localSecond == "00") {
            OnHourChanged?.Invoke();
        }
        OnTimeUpdated?.Invoke($"{localHour}:{localMinute}");
    }

    public void SubscribeOnRealTime(Action<string> method){
        OnTimeUpdated += method;
    }

    public void UnsubscribeOnTimeAlarm(Action<string> method){
        OnTimeUpdated -= method;
    }

    public void SubscribeOnTimeAlarm(Action method) {
        OnHourChanged += method;
    }
    public void UnsubscribeOnTimeAlarm(Action method){
        OnHourChanged -= method;
    }
}
