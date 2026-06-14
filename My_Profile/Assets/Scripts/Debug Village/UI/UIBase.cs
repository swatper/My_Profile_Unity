using UnityEngine;
using Core.Define;

public abstract class UIBase : MonoBehaviour
{
    public abstract void Show();
    public abstract void KeyEvent(KeyEvent key);
    public abstract void Hide();
}
