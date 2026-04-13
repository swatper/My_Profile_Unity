using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Phase
{
    public int MonsterID;
    public int MonsterLevel;
    public int MaxUnit;
}


[CreateAssetMenu(fileName = "Phase", menuName = "ScriptableObject/PhaseData", order = 3)]
public class PhaseData: BaseUpgradeData<Phase>
{}
