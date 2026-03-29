using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : BaseUpgradeModel<WeaponStat>, IUpgradable
{
    [SerializeField] protected WeaponData wData => SOData as WeaponData;
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
        Debug.Log("무기 업글");
        if (!isUnlocked){
            SurvivalSceneDirector.Instance.wHandler.UnlockWeapon(wData.Type);
            isUnlocked = true;
            return;
        }


        base.Upgrade();
        InitWeaponData();
    }

    public override string GetDescription()
    {
        if (isMaxLevel) return "MaxLevel";

        string info = "";

        if (!isUnlocked)
        {
            WeaponStat firstData = wData.levelTables[0]; // 1레벨 데이터
            info += $"    Damage = {firstData.WeaponDamage};\n";
            info += $"    Speed = {firstData.WeaponSpeed:F1};\n";
            if (firstData.PierceCount != 0) {
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
            info += $"    Pierce+= {nextData.PierceCount - curData.PierceCount};\n";

        if (nextData.ScanRange != curData.ScanRange)
            info += $"    Range += {nextData.ScanRange - curData.ScanRange};\n";

        return info + "}";
    }

    protected abstract void Attack();
    protected abstract void InitWeaponData();
}