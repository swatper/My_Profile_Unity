using UnityEngine;
public abstract class BaseCharacterData<T> : BaseUpgradeData<T> where T : struct
{
    [Header("Character Data")]
    public string Name;
    public int ID;
    public string Description;
}
