using UnityEngine;

public class Define : MonoBehaviour
{
    public enum KeyEvent
    {
        Left,
        Right,
        Up,
        Down,
        ESC
    }
    /// <summary>
    /// 언전간 필요하겠지...?
    /// </summary>
    public enum Layers 
    {
        Default,
        BackGround,
        Player_Back,
        Fence,
        Building,
        Player_Front,
        Reserve
    }
}
