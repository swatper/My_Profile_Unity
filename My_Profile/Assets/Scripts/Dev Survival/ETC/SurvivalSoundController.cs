using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalSoundController : MonoBehaviour
{
    [SerializeField] AudioSource bgmSpeaker;
    [SerializeField] AudioSource levelSpeaker;
    [SerializeField] AudioClip bgm;
    [SerializeField] AudioClip[] level;

    public void Init() { 
        bgmSpeaker.clip = bgm;
        PlaySound(bgmSpeaker);
    }

    public void PlayLevelUpEffect(int index) {
        levelSpeaker.clip = level[index];
        PlaySound(levelSpeaker);
    }

    void PlaySound(AudioSource speaker) {
        speaker.Play();
    }

    /// <summary>
    /// Volume만 조절
    /// </summary>
    public void MuteAllSound() {
        bgmSpeaker.volume = 0f;
    }

    /// <summary>
    /// Volume만 조절
    /// </summary>
    public void UnMuteAllSound() {
        bgmSpeaker.volume = 0.3f;
    }
}
