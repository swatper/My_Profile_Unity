using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimeSensitiveControllerBase : MonoBehaviour
{
    protected abstract void CheckTime(Define.TimeOfDay newTime);

    protected virtual void Awake()
    {
        GameManager.Instance.TimeUpdater.SubscribeTimeOfDayAlarm(CheckTime);
    }
    private void OnDestroy()
    {
        if (GameManager.Instance != null){
            GameManager.Instance.TimeUpdater.UnsubscribeTimeOfDayAlarm(CheckTime);
        }
    }
}
