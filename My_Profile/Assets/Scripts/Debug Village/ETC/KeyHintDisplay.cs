using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public abstract class KeyHintDisplay : MonoBehaviour
{
    [Header("입력 키 힌트 설정")]
    [SerializeField] public string targetTag;
    [SerializeField] public  Animator keyAni;
    [SerializeField] string keyType;

    protected  void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != targetTag)
            return;

        GameManager.Input.SubscribeKeyEvent(OnInteract);

        keyAni.Play(keyType);
        OnDetected(collision);
    }
    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != targetTag)
            return;

        GameManager.Input.RemoveSubscribe(OnInteract);

        keyAni.Play("Default");
        OnLost(other);
    }

    protected void OnDestroy()
    {
        //안전장치
        GameManager.Input.RemoveSubscribe(OnInteract);
    }

    /// <summary>
    /// 대상이 탐지 범위에 들어 왔을 때 호출
    /// </summary>
    /// <param name="collision">탐지 대상</param>
    protected virtual void OnDetected(Collider2D collision) { }

    /// <summary>
    /// 대상이 탐지 범위 밖으로 나왔을 때 호출
    /// </summary>
    /// <param name="other">탐지 대상</param>
    protected virtual void OnLost(Collider2D other) { }

    /// <summary>
    /// 탐지 범위 내 상호 작용
    /// </summary>
    /// <param name="keyEvent"></param>
    protected abstract void OnInteract(Define.KeyEvent keyEvent);
}
