using UnityEngine;

public class CppCrossbow : BaseRangedWeapon, ITrackable
{
    [SerializeField] Transform weaponPosition;

    protected override void InitWeaponData()
    {
        base.InitWeaponData();
        weaponPosition.localPosition = new Vector3(0.37f, -0.04f, 0);
    }

    void Reset(){
        if (weaponPosition == null)
            weaponPosition = transform;

        if (mScanner == null)
            mScanner = gameObject.GetComponent<MonsterScanner>();
    }

    protected override void Attack()
    {
        base.Attack();
        //AimAt(mScanner.nearestTarget);
    }

    public  void AimAt(Transform target)
    {
        float targetAngle = Mathf.Atan2(target.position.y, target.position.x) * Mathf.Rad2Deg;
        targetAngle -= 90f;
        weaponPosition.rotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);
    }
}
