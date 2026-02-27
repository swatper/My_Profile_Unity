using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "ScriptableObject/MonsterData", order = 1)]
public class MonsterData : BaseData
{
    public Define.MonsterType MonsterType;
    public int Damage;
    public int DropExp;
}
