using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Exp
{
    public float MaxExp;
}


[CreateAssetMenu(fileName = "Exp", menuName = "ScriptableObject/ExpData", order = 5)]
public class ExpData : ScriptableObject
{
    public List<Exp> levelTables;
}
