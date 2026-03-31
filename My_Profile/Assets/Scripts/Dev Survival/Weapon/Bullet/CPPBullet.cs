using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPPBullet : BaseBullet
{
    [SerializeField] TrailRenderer trail;

    protected override void Awake()
    {
        base.Awake();
        trail = GetComponent<TrailRenderer>();
    }

    public override void Init(float damage, int cnt, Vector3 dir)
    {
        StopAllCoroutines();
        trail.Clear();
        base.Init(damage, cnt, dir);
        StartCoroutine("DeactivateAfterTime");
    }

    protected override void ReturnBullet()
    {
        base.ReturnBullet();
    }
}
