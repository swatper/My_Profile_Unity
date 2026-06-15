using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Define;

public class VillageSoundController : TimeSensitiveControllerBase
{
    [SerializeField] AudioSource bgmBox;
    [SerializeField] AudioSource ambBox;
    [SerializeField] AudioClip[] natureAmbs;
    [SerializeField] TimeOfDay lastTOD = TimeOfDay.Morning;

    protected override void CheckTime(TimeOfDay newTime)
    {
        if (lastTOD != newTime) {
            //Debug.Log("시간대 변경됨");
            switch (newTime) {
                case TimeOfDay.Morning:
                    ambBox.clip = natureAmbs[0];
                    break;
                case TimeOfDay.Day:
                    Debug.Log("오후 음악");
                    break;
                case TimeOfDay.Night:
                    ambBox.clip = natureAmbs[1];
                    break;
            }
            ambBox.Play();
            lastTOD = newTime;
        }
    }

    /// <summary>
    /// Volume만 조절
    /// </summary>
    public void MuteAllSound() {
        bgmBox.volume = 0;
        ambBox.volume = 0;
    }

    /// <summary>
    /// Volume만 조절
    /// </summary>
    public void UnMuteAllSound()
    {
        bgmBox.volume = 0.35f;
        ambBox.volume = 0.95f;
    }
}
