[System.Serializable]
public struct PlayerState
{
    public string pName;
    public int pID;
    public string pDesc;
    public int curHp;
    public float curSpeed;
    public float curMagnetRange;
    public int curExp;
    public bool isDead;
    public bool isReadingInfo;

    public PlayerState(PlayerData data){
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
