using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScanner : MonoBehaviour
{
    [Header("Scanner State")]
    [SerializeField] float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;
    public Transform nearestTarget;

    public void SetScanRange(float range) {
        scanRange = range;
    }

    private void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        nearestTarget = GetNearestMonster();
    }

    public Transform GetNearestMonster() {
        Transform result = null;
        float dist = scanRange;

        foreach (RaycastHit2D target in targets) {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDist = Vector3.Distance(myPos, targetPos);

            if (curDist < dist) {
                dist = curDist;
                result = target.transform; 
            }
        }
        return result;
    }

    //탐지 범위 시각화용
    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        DrowDetectRange();
#endif
    }

#if UNITY_EDITOR
    void DrowDetectRange() {
        Color baseColor = (nearestTarget != null) ? Color.green : Color.red;

        //내부 그리기
        UnityEditor.Handles.color = new Color(baseColor.r, baseColor.g, baseColor.b, 0.15f);
        UnityEditor.Handles.DrawSolidDisc(transform.position, Vector3.forward, scanRange);

        //테두리 그리기 
        UnityEditor.Handles.color = new Color(baseColor.r, baseColor.g, baseColor.b, 1f);
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, scanRange);
    }
#endif
}
