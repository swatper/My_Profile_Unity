using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgradable
{
    public void Upgrade();
    public bool CanUpgrade();
    public bool GetUnlockState();
    /// <summary>
    /// 강화 정보 넘기기
    /// </summary>
    /// <returns></returns>
    public string GetDescription();
}
