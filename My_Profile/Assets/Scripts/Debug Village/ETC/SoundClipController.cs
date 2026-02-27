using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundClipController : TimeSensitiveControllerBase
{
    [SerializeField] AudioSource bgmBox;
    [SerializeField] AudioSource ambBox;
    [SerializeField] AudioClip[] natureAmbs;
    [SerializeField] Define.TimeOfDay lastTOD = Define.TimeOfDay.Morning;

    protected override void CheckTime(Define.TimeOfDay newTime)
    {
        if (lastTOD != newTime) {
            switch (newTime) {
                case Define.TimeOfDay.Morning:
                    ambBox.clip = natureAmbs[0];
                    break;
                case Define.TimeOfDay.Day:
                    Debug.Log("¿ÀÈÄ À½¾Ç");
                    break;
                case Define.TimeOfDay.Night:
                    ambBox.clip = natureAmbs[1];
                    break;
            }
            ambBox.Play();
            lastTOD = newTime;
        }
    }
    public void MuteAllSound() {
        bgmBox.volume = 0;
        ambBox.volume = 0;
    }
    public void UnMuteAllSound()
    {
        bgmBox.volume = 0.35f;
        ambBox.volume = 0.95f;
    }
}
