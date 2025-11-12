using UnityEngine;

public class WANTED_UI : UIBase
{
    [SerializeField]Animator uiAni;
    public override void KeyEvent(Define.KeyEvent key)
    {
        if (key == Define.KeyEvent.ESC)
            Hide();
    }

    public override void Hide(){
        GameManager.Input.RemoveSubscribe(KeyEvent);
        GameManager.Player.isReadingInfo = false;
        gameObject.SetActive(false);
    }

    public override void Show(){
        GameManager.Input.SubscribeKeyEvent(KeyEvent);
        GameManager.Player.isReadingInfo = true;
        gameObject.SetActive(true);
        uiAni.Play("Show");
    }
}
