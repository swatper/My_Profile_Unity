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

    protected void Awake()
    {
        InitWeaponData();
    }

    public virtual void LevelUp() {
        currentLevel++;
        InitWeaponData();
    }

    protected abstract void Attack();
    protected abstract void InitWeaponData();
}