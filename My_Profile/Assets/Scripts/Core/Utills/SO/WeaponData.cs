using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Weapon Struct
/// </summary>
[System.Serializable]
public struct WeaponStat
{
    public float WeaponDamage;
    [Tooltip("공격 주기")]
    public float WeaponSpeed;
    public int PierceCount;
    public int ScanRange;
}


/// <summary>
/// Weapon Scriptable Object
/// </summary>
[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObject/WeaopnData", order = 2)]
public class WeaponData : BaseUpgradeData
{
    public string WeaponName;
    public Sprite WeaponIcon;
    public Define.UpgradeType Type;
    public List<WeaponStat> levelTables;
}