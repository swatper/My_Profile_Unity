using UnityEngine;

public class BaseRangedWeapon : BaseWeapon
{
    [Header("Ranged Weapon Info")]
    [SerializeField] protected int bID;
    [SerializeField] protected float timer;
    [Tooltip("발사체 속도")]
    [SerializeField] protected float projectileSpeed;
    [Header("Weapon Component")]
    [SerializeField] protected MonsterScanner mScanner;

    protected override void InitWeaponData(){
        timer = 1.0f;   //첫 공격은 바로 공격
        wStat = wData.levelTables[currentLevel - 1];
        mScanner.SetScanRange(wStat.ScanRange);
    }

    private void Update()
    {
        if (mScanner.nearestTarget == null)
            return;
        Attack();
    }

    protected override void Attack() {
        timer += Time.deltaTime * wStat.WeaponSpeed;
        if (timer < 1.0f)
            return;
        //쿨타임 적용
        timer = 0f;

        //탄 요청: 비활성화 상태
        BaseBullet bullet = SurvivalSceneDirector.Instance.poolManager.GetBulletFromPool(bID);

        if (bullet == null)
            return;
            

        //방향 조절
        Vector3 targetPos = mScanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        //초기화 및 발사: 활성화 상태
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.Init(wStat.WeaponDamage, wStat.PierceCount, dir * projectileSpeed);
    }
}
