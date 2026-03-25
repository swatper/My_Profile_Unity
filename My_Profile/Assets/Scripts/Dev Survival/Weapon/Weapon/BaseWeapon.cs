using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
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

    public virtual void LevelUp() {
        if (isMaxLevel) return;
        currentLevel++;
        InitWeaponData();

        if (currentLevel >= wData.levelTables.Count){
            isMaxLevel = true;
            Debug.Log($"무기 만렙 달성: {currentLevel}");
        }
        else
            Debug.Log($"무기 업그레이드 완료: {currentLevel}");
    }

    /// <summary>
    /// 무기 능력치 강화 정보 넘기기
    /// </summary>
    /// <returns></returns>
    public string UpgradeInfo() {
        if (isMaxLevel) return "MaxLevel";

        string info = "";

        if (!isUnlocked)
        {
            WeaponStat firstData = wData.levelTables[0]; // 1레벨 데이터
            info += $"    damage = {firstData.WeaponDamage};\n";
            info += $"    attackSpeed = {firstData.WeaponSpeed:F1};\n";
            info += $"    pierceCount = {firstData.PierceCount};\n";
            info += $"    scanRange = {firstData.ScanRange};\n";
            return info + "}";
        }

        WeaponStat nextData = wData.levelTables[currentLevel];
        WeaponStat curData = wData.levelTables[currentLevel - 1];
        // 수치가 변한 것만 코드 형태로 추가
        if (nextData.WeaponDamage != curData.WeaponDamage)
            info += $"    damage += {nextData.WeaponDamage - curData.WeaponDamage};\n";

        if (nextData.WeaponSpeed != curData.WeaponSpeed)
            info += $"    attackSpeed += {nextData.WeaponSpeed - curData.WeaponSpeed:F1};\n";

        if (nextData.PierceCount != curData.PierceCount)
            info += $"    pierceCount += {nextData.PierceCount - curData.PierceCount};\n";

        if (nextData.ScanRange != curData.ScanRange)
            info += $"    scanRange += {nextData.ScanRange - curData.ScanRange};\n";

        return info + "}";
    }

    protected abstract void Attack();
    protected abstract void InitWeaponData();
}