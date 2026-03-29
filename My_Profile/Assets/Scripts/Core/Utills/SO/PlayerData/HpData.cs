using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Hp
{
    public float MaxHp;
}

[CreateAssetMenu(fileName = "Hp", menuName = "ScriptableObject/Player/Hp", order = 2)]
public class HpData : BaseUpgradeData<Hp> {}
