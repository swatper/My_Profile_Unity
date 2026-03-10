using UnityEngine;

[System.Serializable]
public struct PlayerState
{
    public string pName;
    public int pID;
    public string pDesc;
    public float curHp;
    public float curSpeed;
    public float curMagnetRange;
    public int curExp;
    public bool isDead;
    public bool isReadingInfo;

    public PlayerState(PlayerData data)
    {
        pName = data.name;
        pDesc = data.Description;
        curHp = data.MaxHP;
        curSpeed = data.MoveSpeed;
        curMagnetRange = data.MagnetRange;
        pID = data.SpriteNum;
        curExp = 0;
        isDead = false;
        isReadingInfo = false;
    }
}


[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObject/PlayerData", order = 1)]
public class PlayerData : BaseData
{
    public int SpriteNum;
    public float MagnetRange;
}