using UnityEngine;

public class CppCrossbow : BaseRangedWeapon, ITrackable
{
    [SerializeField] Transform weaponPosition;

    protected override void Attack()
    {
        if (mScanner.nearestTarget == null)
            return;

        base.Attack();
        //AimAt(mScanner.nearestTarget);
    }
    public  void AimAt(Transform target)
    {
        Vector3 targetDirection = target.position - weaponPosition.position;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        weaponPosition.rotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);
    }
}
