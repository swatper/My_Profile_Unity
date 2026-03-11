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

    protected void Awake()
    {
        InitWeaponData();
    }

    public virtual void LevelUp() {
        currentLevel++;
        if (currentLevel >wData.levelTables.Count) {
            Debug.Log("이미 최대 레벨");
            return;
        }
        InitWeaponData();
        Debug.Log($"무기 업그레이드 완료: {currentLevel}");
    }

    protected abstract void Attack();
    protected abstract void InitWeaponData();
}