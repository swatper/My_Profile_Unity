using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObject/WeaopnData", order = 0)]
public class WeaponData : ScriptableObject
{
    public string WeaponName;
    public int WeaponDamage;
    public int WeaponSpeed;
}
