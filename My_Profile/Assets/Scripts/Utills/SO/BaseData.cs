using UnityEngine;
public  abstract class BaseData : ScriptableObject
{
    [Header("Character Data")]
    public string Name;
    public string Description;
    public int MaxHP;
    public float MoveSpeed;
}
