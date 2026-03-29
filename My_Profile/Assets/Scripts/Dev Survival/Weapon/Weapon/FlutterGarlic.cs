using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlutterGarlic : BaseWeapon
{
    public Transform obj;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;
    [SerializeField] float timer;

    private void Update()
    {
        timer += Time.deltaTime * wStat.WeaponSpeed;
        if (timer > 1.0f)
        {
            timer = 0f;
            Attack();
        }
    }

    protected override void InitWeaponData(){
        wStat = wData.levelTables[currentLevel - 1];
        UpdateEffects();
    }

    protected override void Attack()
    {
        targets = Physics2D.CircleCastAll(transform.position, wStat.ScanRange, Vector2.zero, 0, targetLayer);
        foreach (RaycastHit2D hit in targets)
        {
            if (hit.collider.TryGetComponent(out BaseMonsterController enemy))
            {
                enemy.OnHit(wStat.WeaponDamage, false);
            }
        }
    }

    void UpdateEffects() {
        float effectScale = wStat.ScanRange * 2;
        obj.localScale = new Vector3(effectScale, effectScale, 1f);
    }


#if UNITY_EDITOR
    //탐지 범위 시각화용
    private void OnDrawGizmos()
    {
        DrowDetectRange();
    }

    void DrowDetectRange()
    {
        Color baseColor = Color.blue;

        //내부 그리기
        UnityEditor.Handles.color = new Color(baseColor.r, baseColor.g, baseColor.b, 0.15f);
        UnityEditor.Handles.DrawSolidDisc(transform.position, Vector3.forward, wStat.ScanRange);

        //테두리 그리기 
        UnityEditor.Handles.color = new Color(baseColor.r, baseColor.g, baseColor.b, 1f);
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, wStat.ScanRange);
    }
#endif
}
