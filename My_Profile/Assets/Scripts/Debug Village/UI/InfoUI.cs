using UnityEngine;
using Core.Define;

public class InfoUI : UIBase
{
    [SerializeField]Animator uiAni;
    public override void KeyEvent(KeyEvent key)
    {
        if (key == Core.Define.KeyEvent.ESC)
            Hide();
    }

    public override void Hide(){
        if (uiAni == null)
            return;

        GameManager.Input.RemoveSubscribe(KeyEvent);
        GameManager.Player.StopReadUIInfo();
        gameObject.SetActive(false);
    }

    public override void Show(){
        if (uiAni == null)
            return;

        GameManager.Input.SubscribeKeyEvent(KeyEvent);
        GameManager.Player.ReadUIInfo();
        gameObject.SetActive(true);
        uiAni.Play("Show");
    }
}
