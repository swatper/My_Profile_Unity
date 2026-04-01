using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 실제 업그레이드 기능이 있는 클래스
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseUpgradeModel <T>: MonoBehaviour, IUpgradable where T : struct
{
    [Header("SO Data")]
    public BaseUpgradeData<T> SOData;
    [Header("Current Stat")]
    public T currentStat;
    [SerializeField] protected int currentLevel = 1;
    public bool isMaxLevel = false;
    public bool isUnlocked = false;

    public  virtual void Upgrade()
    {
        if (isMaxLevel)
            return;
        currentLevel++;

        if (currentLevel >= SOData.levelTables.Count)
            isMaxLevel = true;
    }

    public virtual bool CanUpgrade() => isMaxLevel;

    public virtual bool GetUnlockState() => isUnlocked;

    public  virtual string GetDescription(){
        return "";
    }

    public virtual Sprite GetIcon(){
        throw new System.NotImplementedException();
    }
}
