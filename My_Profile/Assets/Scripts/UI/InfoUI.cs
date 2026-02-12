using UnityEngine;

public class InfoUI : UIBase
{
    [SerializeField]Animator uiAni;
    public override void KeyEvent(Define.KeyEvent key)
    {
        if (key == Define.KeyEvent.ESC)
            Hide();
    }

    public override void Hide(){
        GameManager.Input.RemoveSubscribe(KeyEvent);
        GameManager.Player.StopReadUIInfo();
        gameObject.SetActive(false);
    }

    public override void Show(){
        GameManager.Input.SubscribeKeyEvent(KeyEvent);
        GameManager.Player.ReadUIInfo();
        gameObject.SetActive(true);
        uiAni.Play("Show");
    }
}
