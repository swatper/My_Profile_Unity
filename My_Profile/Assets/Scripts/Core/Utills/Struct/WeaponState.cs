[System.Serializable]
public struct WeaponState
{
    public string wName;
    public float wDamage;
    public int wSpeed;
    public int pCount;
    public int wRange;

    public WeaponState(WeaponData data) { 
        wName = data.WeaponName;
        wDamage = data.WeaponDamage;
        wSpeed = data.WeaponSpeed;
        pCount = data.PierceCount;
        wRange = data.ScanRange;
    }
}
