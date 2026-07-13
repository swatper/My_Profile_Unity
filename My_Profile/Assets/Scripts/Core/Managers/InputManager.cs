using System;
using UnityEngine;
using  Core.Define;
using UnityEngine.InputSystem; //새로운 InputSystem 사용

public class InputManager
{
    public Action<KeyEvent> KeyPress;
    public Keyboard keyboard;

    public InputManager()
    {
        //생성자에서 최초 할당 및 디바이스 연결 이벤트 등록
        RefreshKeyboard();
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    //디바이스가 새로 연결되거나 끊겼을 때 감지 (안전장치)
    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (device is Keyboard)
            RefreshKeyboard();
    }

    private void RefreshKeyboard(){
        keyboard = Keyboard.current;
    }

    public void OnKeyEvent()
    {
        if (KeyPress == null)
            return;

        keyboard = Keyboard.current;
        if (keyboard == null){
            Debug.Log("키보들을 찾을 수 없음");
            return;
        }

        #region 플레이어 조작
        //←
        if (keyboard.leftArrowKey.isPressed || keyboard.aKey.isPressed) 
            KeyPress.Invoke(KeyEvent.Left);
        //→
        if (keyboard.rightArrowKey.isPressed || keyboard.dKey.isPressed)
            KeyPress.Invoke(KeyEvent.Right);
        //↑
        if (keyboard.upArrowKey.isPressed || keyboard.wKey.isPressed)
            KeyPress.Invoke(KeyEvent.Up);
        //↓
        if (keyboard.downArrowKey.isPressed || keyboard.sKey.isPressed)
            KeyPress.Invoke(KeyEvent.Down);
        #endregion

        #region 기다 타른 키
        //ESC
        if (keyboard.escapeKey.wasPressedThisFrame)
            KeyPress.Invoke(KeyEvent.ESC);
        //Tab
        if (keyboard.tabKey.wasPressedThisFrame) 
            KeyPress.Invoke(KeyEvent.Tab);
        //Enter
        if (keyboard.enterKey.wasPressedThisFrame || keyboard.numpadEnterKey.wasPressedThisFrame) 
            KeyPress.Invoke(KeyEvent.Enter);
        //F12
        if (keyboard.f2Key.wasPressedThisFrame)
            KeyPress.Invoke(KeyEvent.Debug);
        #endregion
    }

    /// <summary>
    /// 외부 키 용
    /// </summary>
    /// <param name="type"></param>
    public void RequestKeyEvent(KeyEvent type){
        KeyPress?.Invoke(type);
    }

    public void SubscribeKeyEvent(Action<KeyEvent> tragetMethod) {
        KeyPress += tragetMethod;
    }

    public void RemoveSubscribe(Action<KeyEvent> tragetMethod) {
        KeyPress -= tragetMethod;
    }
}