using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerState
{
    public string pName;
    public string pDesc;
    public float curMagnetRange;
    public int curExp;
    public bool isDead;
    public bool isReadingInfo;

    public PlayerState(PlayerData data)
    {
        pName = data.name;
        pDesc = data.Description;
        curMagnetRange = data.MagnetRange;
        curExp = 0;
        isDead = false;
        isReadingInfo = false;
    }
}


[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObject/Player/PlayerData", order = 1)]
public class PlayerData : BaseData<float>
{
    public float MagnetRange;
    public HpData Hp;
    public SpeedData Speed;
}