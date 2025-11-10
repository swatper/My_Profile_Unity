using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Action<Define.KeyEvent> KeyPress;

    public void KeyEvent()
    {
        if (KeyPress == null)
            return;

        //LeftArrow
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            KeyPress.Invoke(Define.KeyEvent.Left);
        }
        //RightArrow
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            KeyPress.Invoke(Define.KeyEvent.Right);
        }
        //UpArrow
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            KeyPress.Invoke(Define.KeyEvent.Up);
        }
        //DownArrow
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            KeyPress.Invoke(Define.KeyEvent.Down);
        }
    }

    public void SubscribeKeyEvent(Action<Define.KeyEvent> tragetMethod) {
        KeyPress += tragetMethod;
    }

    public void RemveSubscribe(Action<Define.KeyEvent> tragetMethod) {
        KeyPress -= tragetMethod;
    }
}