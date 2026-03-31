using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityBook : BaseWeapon
{
    [SerializeField] BaseBullet[] unityBullets;
    float baseRotateSpeed = 360f;
    [SerializeField] float curSpeed;

    private void Update(){
        transform.Rotate(Vector3.forward * curSpeed * Time.deltaTime, Space.World);
    }

    protected override void Attack(){}

    protected override void InitWeaponData()
    {
        //능력치 설정
        wStat = wData.levelTables[currentLevel - 1];
        curSpeed = baseRotateSpeed * wStat.WeaponSpeed;

        //배치
        float angleStep = 360f / unityBullets.Length;
        for (int i = 0; i < unityBullets.Length; i++) {
            float angle = i * angleStep;
            Vector3 spawnPos = new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad) * wStat.ScanRange,
                Mathf.Sin(angle * Mathf.Deg2Rad) * wStat.ScanRange,
                0
            );

            //투사체 배치 및 초기화
            unityBullets[i].transform.localPosition = spawnPos;
            unityBullets[i].Init(1, -1);
        }
    }
}
