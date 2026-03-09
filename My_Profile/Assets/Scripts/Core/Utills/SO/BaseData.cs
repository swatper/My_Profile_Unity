using UnityEngine;
public  abstract class BaseData : ScriptableObject
{
    [Header("Character Data")]
    public string Name;
    public string Description;
    //추후 삭제할 예정
    public float MaxHP;
    public float MoveSpeed;
}
