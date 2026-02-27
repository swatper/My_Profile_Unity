using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimeSensitiveControllerBase : MonoBehaviour
{
    protected abstract void CheckTime(Define.TimeOfDay newTime);

    protected virtual void Awake()
    {
        GameManager.Instance.OnTimeOfDayChanged += CheckTime;
        CheckTime(GameManager.Instance.curTOD);
    }
    private void OnDestroy()
    {
        if (GameManager.Instance != null){
            GameManager.Instance.OnTimeOfDayChanged -= CheckTime;
        }
    }
}
