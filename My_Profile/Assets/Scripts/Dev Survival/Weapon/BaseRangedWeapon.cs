using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRangedWeapon : BaseWeapon
{
    [Header("Ranged Weapon Info")]
    [SerializeField] int bID;
    [SerializeField] float timer;
    [Tooltip("발사체 속도")]
    [SerializeField] float projectileSpeed = 15f;
    [Header("Weapon Component")]
    [SerializeField] MonsterScanner mScanner;

    protected override void InitWeaponData(){
        wStat = wData.levelTables[currentLevel - 1];
        mScanner.SetScanRange(wStat.ScanRange);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > wStat.WeaponSpeed) {
            timer = 0f;
            Attack();
        }
    }

    protected override void Attack() {
        if (!mScanner.nearestTarget)
            return;
        //방향 조절
        Vector3 targetPos = mScanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        //탄 생성
        Bullet bullet = SurvivalSceneDirector.Instance.poolManager.GetBulletFromPool(bID);

        //초기화 및 발싸
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.Init(wStat.WeaponDamage, wStat.PierceCount, dir * projectileSpeed);
    }
}
