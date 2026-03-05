using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPPCorssbow : MonoBehaviour
{
    [Header("Weapon Info")]
    [Tooltip("무기 기본 능력치")]
    [SerializeField] WeaponData wData;
    [Tooltip("실시간 데이터 처리용")]
    [SerializeField] WeaponState wState;
    [SerializeField] float projectileSpeed = 15f;
    float timer;
    [SerializeField] int bID;
    [Header("Weapon Component")]
    [SerializeField] MonsterScanner mScanner;

    private void Awake(){
        SetWeaponData(wData);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > wState.wSpeed) {
            timer = 0f;
            Fire();
        }
    }

    void Fire() {
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
        bullet.Init(wState.wDamage, wState.pCount, dir * projectileSpeed);
    }

    void SetWeaponData(WeaponData newData){
        wState = new WeaponState(newData);
        mScanner.SetScanRange(wState.wRange);
    }

    public void LevelUp(float damage, int cnt) {
        wState.wDamage += damage;
        wState.pCount += cnt;
    }
}
