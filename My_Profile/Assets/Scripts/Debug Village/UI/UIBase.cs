using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBase : MonoBehaviour
{
    public abstract void Show();
    public abstract void KeyEvent(Define.KeyEvent key);
    public abstract void Hide();
}
