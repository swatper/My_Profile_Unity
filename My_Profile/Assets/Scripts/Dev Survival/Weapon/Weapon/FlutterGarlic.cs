using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlutterGarlic : BaseRangedWeapon
{
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;

    protected override void InitWeaponData(){
        wStat = wData.levelTables[currentLevel - 1];
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
