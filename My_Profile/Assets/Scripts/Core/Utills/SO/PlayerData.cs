using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObject/PlayerData", order = 2)]
public class PlayerData : BaseData
{
    public int SpriteNum;
    public float MagnetRange;
}
