using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour, IUpgradable
{
    [Header("Weapon Data")]
    [SerializeField] protected WeaponData wData;
    [Header("Current Weapon Data")]
    [SerializeField] protected WeaponStat wStat;
    [SerializeField] protected int currentLevel = 1;
    public bool isUnlocked = false;
    public bool isMaxLevel = false;

    protected void Awake(){
        InitWeaponData();
    }

    /// <summary>
    ///기본 업그레이드 로직
    /// </summary>
    public virtual void Upgrade()
    {
        Debug.Log("무기 업글");
        if (isMaxLevel) return;
        if (!isUnlocked) {
            SurvivalSceneDirector.Instance.wHandler.UnlockWeapon(wData.Type);
            isUnlocked = true;
            return;
        }

        currentLevel++;
        InitWeaponData();

        if (currentLevel >= wData.levelTables.Count){
            isMaxLevel = true;
            Debug.Log($"무기 만렙 달성: {currentLevel}");
        }
    }

    public bool CanUpgrade(){
        return isMaxLevel;
    }
    public bool GetUnlockState(){
        return !isUnlocked;
    }


    public virtual string GetDescription()
    {
        if (isMaxLevel) return "MaxLevel";

        string info = "";

        if (!isUnlocked)
        {
            WeaponStat firstData = wData.levelTables[0]; // 1레벨 데이터
            info += $"    Damage = {firstData.WeaponDamage};\n";
            info += $"    Speed = {firstData.WeaponSpeed:F1};\n";
            info += $"    Pierce = {firstData.PierceCount};\n";
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