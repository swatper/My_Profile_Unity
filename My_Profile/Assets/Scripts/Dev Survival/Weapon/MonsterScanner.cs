using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScanner : MonoBehaviour
{
    [Header("Scanner State")]
    float scanRange;
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

}
