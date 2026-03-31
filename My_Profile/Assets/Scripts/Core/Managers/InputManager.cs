using System;
using UnityEngine;
using static Define;
public class InputManager
{
    public Action<KeyEvent> KeyPress;

    public void OnKeyEvent()
    {
        if (KeyPress == null)
            return;

        //LeftArrow
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            KeyPress.Invoke(KeyEvent.Left);
        }
        //RightArrow
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            KeyPress.Invoke(KeyEvent.Right);
        }
        //UpArrow
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            KeyPress.Invoke(KeyEvent.Up);
        }
        //DownArrow
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            KeyPress.Invoke(KeyEvent.Down);
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            KeyPress.Invoke(KeyEvent.ESC);
        }
        if (Input.GetKey(KeyCode.Tab)) {
            KeyPress.Invoke(KeyEvent.Tab);
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
            KeyPress.Invoke(KeyEvent.Enter);
        }
        if (Input.GetKeyDown(KeyCode.F2)) {
            KeyPress.Invoke(KeyEvent.Debug);
        }
    }

    public void SubscribeKeyEvent(Action<KeyEvent> tragetMethod) {
        KeyPress += tragetMethod;
    }

    public void RemoveSubscribe(Action<KeyEvent> tragetMethod) {
        KeyPress -= tragetMethod;
    }
}