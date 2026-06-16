using UnityEngine;
using Core.Define;

[System.Serializable]
public class SpeedStat : BaseUpgradeModel<Speed>
{
    public UpgradeType upgradeType;

    public override string GetDescription()
    {
        if (isMaxLevel) return "MaxLevel";

        Speed nextData = SOData.levelTables[currentLevel];
        Speed curData = SOData.levelTables[currentLevel - 1];
        string info = "";

        info += $"    {upgradeType.ToString()} += {nextData.spd - curData.spd}";

        return info + "}";
    }

    public override Sprite GetIcon(){
        return null;
    }
}
