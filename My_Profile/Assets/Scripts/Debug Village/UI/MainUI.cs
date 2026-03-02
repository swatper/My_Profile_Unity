using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [Tooltip("현재 위치 및 시간 정보")]
    [SerializeField] Text clock;
    [SerializeField] Text location;

    private void Start(){
        GameManager.Clock.SubscribeOnRealTime(UpdateTime);
    }

    void UpdateTime(string Time) {
        clock.text = Time;
    }
}
