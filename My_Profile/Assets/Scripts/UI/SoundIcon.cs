using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundIcon : MonoBehaviour
{
    [SerializeField] Image soundIcon;
    [SerializeField] Sprite[] iconSprites;
    [SerializeField] SoundClipController soundController;
    [SerializeField] bool IsMute = false;


    public void SoundOnOff()
    {
        IsMute = !IsMute;
        if (IsMute){
            soundController.MuteAllSound();
            soundIcon.sprite = iconSprites[1];
        }
        else {
            soundController.UnMuteAllSound();
            soundIcon.sprite = iconSprites[0];
        }
    }
}
