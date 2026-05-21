using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Define;

[System.Serializable]
public class HpStat : BaseUpgradeModel<Hp>
{
    public UpgradeType upgradeType;

    public override string GetDescription()
    {
        if (isMaxLevel) return "MaxLevel";

        Hp nextData = SOData.levelTables[currentLevel];
        Hp curData = SOData.levelTables[currentLevel - 1];
        string info = "";

        info += $"    {upgradeType.ToString()} += {nextData.MaxHp - curData.MaxHp}";

        return info + "}";
    }

}
