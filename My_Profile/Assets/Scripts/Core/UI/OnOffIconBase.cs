using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class OnOffIconBase : MonoBehaviour
{
    [SerializeField] public Image buttonIcon;
    [Tooltip("0: Defualit")]
    [SerializeField] public Sprite[] iconSprites;
    [SerializeField] public bool IsOn = false;

    /// <summary>
    /// 외부 버튼
    /// </summary>
    public void PressButton(){
        IsOn = !IsOn;
        CheckSprite();
    }

    /// <summary>
    /// 씬 디렉터용
    /// </summary>
    public virtual void InitStatus() {
        CheckSprite();
    }


    public virtual void CheckSprite() {
        if (!IsOn)
            buttonIcon.sprite = iconSprites[0];
        else
            buttonIcon.sprite = iconSprites[1];

        ActionalProcessing();
    }

    protected abstract void ActionalProcessing();
}
