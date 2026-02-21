using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class OnOffIconBase : MonoBehaviour
{
    [SerializeField] public Image buttonIcon;
    [SerializeField] public Sprite[] iconSprites;
    [SerializeField] public bool IsOn = false;
    public void PressButton()
    {
        IsOn = !IsOn;
        if (IsOn)
            buttonIcon.sprite = iconSprites[1];
        else
            buttonIcon.sprite = iconSprites[0];

        AddtionalProcessing();
    }

    protected virtual void AddtionalProcessing() { }

}
