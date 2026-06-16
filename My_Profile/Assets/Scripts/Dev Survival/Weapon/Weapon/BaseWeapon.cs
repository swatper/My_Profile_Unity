using UnityEngine;

public abstract class BaseWeapon : BaseUpgradeModel<WeaponStat>
{
    protected WeaponData wData => SOData as WeaponData;
    protected WeaponStat wStat{
        get => currentStat;
        set => currentStat = value;
    }

    protected void Awake(){
        InitWeaponData();
    }

    /// <summary>
    ///기본 업그레이드 로직
    /// </summary>
    public override void Upgrade()
    {
        if (!isUnlocked){
            SurvivalSceneDirector.Instance.wHandler.UnlockWeapon(wData.Type);
            isUnlocked = true;
            return;
        }

        base.Upgrade();
        InitWeaponData();
    }

    /// <summary>
    /// 업그레이드 슬롯에 보여줄 무기 능력치 강화 수치
    /// </summary>
    public override string GetDescription()
    {
        if (isMaxLevel) return "MaxLevel";

        string info = "";

        //아이템을 해금할 경우
        if (!isUnlocked)
        {
            WeaponStat firstData = wData.levelTables[0]; // 1레벨 데이터
            info += $"    Damage = {firstData.WeaponDamage};\n";
            info += $"    Speed = {firstData.WeaponSpeed:F1};\n";
            if (firstData.PierceCount > 0) {
                info += $"    Pierce = {firstData.PierceCount};\n";
            }
            info += $"    Range = {firstData.ScanRange};\n";
            return info + "}";
        }

        WeaponStat nextData = wData.levelTables[currentLevel];
        WeaponStat curData = wData.levelTables[currentLevel - 1];
        //수치가 변한 것만 코드 형태로 추가
        if (nextData.WeaponDamage != curData.WeaponDamage)
            info += $"    Damage += {nextData.WeaponDamage - curData.WeaponDamage};\n";

        if (nextData.WeaponSpeed != curData.WeaponSpeed)
            info += $"    Speed += {nextData.WeaponSpeed - curData.WeaponSpeed:F1};\n";

        if (nextData.PierceCount != curData.PierceCount)
            info += $"    Pierce += {nextData.PierceCount - curData.PierceCount};\n";

        if (nextData.ScanRange != curData.ScanRange)
            info += $"    Range += {nextData.ScanRange - curData.ScanRange};\n";

        return info + "}";
    }

    public override Sprite GetIcon(){
        return wData.WeaponIcon;
    }

    protected abstract void Attack();
    protected abstract void InitWeaponData();
}