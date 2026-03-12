using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct LoadingMessage
{
    public string SceneName;
    public string StartMessage;
    public string EndMessage;
}

/// <summary>
/// Weapon Scriptable Object
/// </summary>
[CreateAssetMenu(fileName = "LoadingMessage", menuName = "ScriptableObject/LoadingMessage", order = 5)]
public class LoadingMessageData : ScriptableObject
{
    public List<LoadingMessage> messageTable;
}