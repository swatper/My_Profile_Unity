using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public abstract class KeyHintDisplay : MonoBehaviour
{
    [SerializeField]public  Animator keyAni;
    [SerializeField] string keyType;

    protected void Awake()
    {
        GameManager.Input.SubscribeKeyEvent(OnInteract);
    }

    protected void OnDestroy()
    {
        GameManager.Input.RemoveSubscribe(OnInteract);
    }

    protected  void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "ETC")
            return;
        keyAni.Play(keyType);
        OnDetected(collision);
    }
    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != "ETC")
            return;
        keyAni.Play("Default");
        OnLost(other);
    }

    protected virtual void OnDetected(Collider2D collision) { }

    protected virtual void OnLost(Collider2D other) { }

    protected virtual void OnInteract(Define.KeyEvent keyEvent) { }
}
