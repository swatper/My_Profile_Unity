using System;
using UnityEngine;

public class InputManager
{
    public Action<Define.KeyEvent> KeyPress;

    public void KeyEvent()
    {
        if (KeyPress == null)
            return;

        //LeftArrow
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            KeyPress.Invoke(Define.KeyEvent.Left);
        }
        //RightArrow
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            KeyPress.Invoke(Define.KeyEvent.Right);
        }
        //UpArrow
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            KeyPress.Invoke(Define.KeyEvent.Up);
        }
        //DownArrow
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            KeyPress.Invoke(Define.KeyEvent.Down);
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            KeyPress.Invoke(Define.KeyEvent.ESC);
        }
        if (Input.GetKey(KeyCode.Tab)) {
            KeyPress.Invoke(Define.KeyEvent.Tab);
        }
    }

    public void SubscribeKeyEvent(Action<Define.KeyEvent> tragetMethod) {
        KeyPress += tragetMethod;
    }

    public void RemoveSubscribe(Action<Define.KeyEvent> tragetMethod) {
        KeyPress -= tragetMethod;
    }
}