using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SO 스크립트
/// </summary>
/// <typeparam name="T">구조체</typeparam>
[System.Serializable]
public abstract class BaseUpgradeData<T> : ScriptableObject where T : struct{
    [Header("Level Table")]
    public List<T> levelTables;
}