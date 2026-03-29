using UnityEngine;
public  abstract class BaseData<T> : BaseUpgradeData<T> where T: struct
{
    [Header("Character Data")]
    public string Name;
    public string Description;
    //추후 삭제할 예정
    public float MaxHP;
    public float MoveSpeed;
}
