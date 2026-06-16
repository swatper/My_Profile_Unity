using UnityEngine;

/// <summary>
/// 실제 업그레이드 기능이 있는 클래스
/// </summary>
/// <typeparam name="T">업드레이드 데이터 테이블 타입</typeparam>
public abstract class BaseUpgradeModel <T>: MonoBehaviour, IUpgradable where T : struct
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
        Debug.Log($"업그레이드: {gameObject.name}");
        currentLevel++;

        if (currentLevel >= SOData.levelTables.Count)
            isMaxLevel = true;
    }

    public virtual bool CanUpgrade() => !isMaxLevel;

    public virtual bool GetUnlockState() => isUnlocked;

    public abstract string GetDescription();

    public abstract Sprite GetIcon();
}
