using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MonsterStat
{
    public float curHp;
    public float curSpeed;
    public float Damage;
    public float KnockBack;
    public float Exp;
    public bool isDead;
}


[CreateAssetMenu(fileName = "Monster", menuName = "ScriptableObject/MonsterData", order = 0)]
public class MonsterData : BaseData
{
    public Define.MonsterType MonsterType;
    public int MonsterID;
    public List<MonsterStat> levelTables;
}
