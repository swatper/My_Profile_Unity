using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundClipController : TimeSensitiveControllerBase
{
    [SerializeField] AudioSource amb;
    [SerializeField] AudioClip birdAmb;
    [SerializeField] AudioClip wolfAmb;
    [SerializeField] Define.TimeOfDay lastTOD = Define.TimeOfDay.Morning;

    protected override void CheckTime(Define.TimeOfDay newTime)
    {
        if (lastTOD != newTime) {
            switch (newTime) {
                case Define.TimeOfDay.Morning:
                    amb.clip = birdAmb;
                    amb.Play();
                    break;
                case Define.TimeOfDay.Day:
                    Debug.Log("음악 없음");
                    break;
                case Define.TimeOfDay.Night:
                    Debug.Log("음악 없음");
                    //음악이 없으므로 일단 정지
                    amb.Stop();
                    break;
            }
            lastTOD = newTime;
        }
    }
}
