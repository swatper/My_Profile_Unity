using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Pase
{
    public int MonsterID;
    public int MonsterLevel;
    public int MaxUnit;
}


[CreateAssetMenu(fileName = "LevelDesign", menuName = "ScriptableObject/PaseData", order = 3)]
public class PaseData: ScriptableObject
{
    public List<Pase> levelTables;
}
