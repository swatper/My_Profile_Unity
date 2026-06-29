using UnityEngine;
using UnityEngine.UI;

public abstract class BaseOnOffIconController : MonoBehaviour
{
    [SerializeField] public Image buttonIcon;
    [Tooltip("0: Defualit")]
    [SerializeField] public Sprite[] iconSprites;
    [Tooltip("인스펙터 창에서 확인용")]
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

    /// <summary>
    /// 스프라이트 변경 및 이후 기능 수행
    /// </summary>
    public virtual void CheckSprite() {
        if (!IsOn)
            buttonIcon.sprite = iconSprites[0];
        else
            buttonIcon.sprite = iconSprites[1];

        ActionalProcessing();
    }

    /// <summary>
    /// 기능 로직
    /// </summary>
    protected abstract void ActionalProcessing();
}