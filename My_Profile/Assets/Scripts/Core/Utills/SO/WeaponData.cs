using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObject/WeaopnData", order = 0)]
public class WeaponData : ScriptableObject
{
    public string WeaponName;
    public float WeaponDamage;
    public int WeaponSpeed;
    public int PierceCount;
    public int ScanRange;
}
