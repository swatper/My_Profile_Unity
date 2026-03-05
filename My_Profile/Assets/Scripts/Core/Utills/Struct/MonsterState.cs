[System.Serializable]
public struct MonsterState
{
    public string mName;
    public Define.MonsterType mType;
    public string mDesc;
    public float curHp;
    public float curSpeed;
    public float Damage;
    public float Exp;
    public bool isDead;

    public MonsterState(MonsterData data) {
        mName = data.Name;
        mType = data.MonsterType;
        mDesc = data.Description;
        curHp = data.MaxHP;
        curSpeed = data.MoveSpeed;
        Damage = data.Damage;
        Exp = data.DropExp;
        isDead = false;
    }
}
