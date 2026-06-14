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

    /// <summary>
    /// 스프라이트 변경 및 이후 작업 호출
    /// </summary>
    /// <param name="isInit">fasle: 스프라이트만 변경</param>
    public virtual void CheckSprite(bool isInit = false) {
        if (!IsOn)
            buttonIcon.sprite = iconSprites[0];
        else
            buttonIcon.sprite = iconSprites[1];

        if(!isInit)
            ActionalProcessing();
    }

    protected abstract void ActionalProcessing();
}