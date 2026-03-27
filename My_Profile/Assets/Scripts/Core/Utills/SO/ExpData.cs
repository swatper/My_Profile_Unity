using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Exp
{
    public float MaxExp;
}


[CreateAssetMenu(fileName = "Exp", menuName = "ScriptableObject/ExpData", order = 4)]
public class ExpData : BaseUpgradeData
{
    public List<Exp> levelTables;
}
