using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSharpBullet : BaseBullet
{
    [SerializeField] ParticleSystem trailParticle;
    [SerializeField] float explosionRadius;
    public LayerMask targetLayer;

    protected override void Awake()
    {
        base.Awake();
        trailParticle = GetComponentInChildren<ParticleSystem>();
    }

    public override void Init(float damage, int cnt, Vector3 dir){
        StopAllCoroutines();
        trailParticle.Clear();
        trailParticle.Play();
        base.Init(damage, cnt, dir);
        StartCoroutine("DeactivateAfterTime");
    }

    public override void HitMonster(BaseMonsterController monster){
        Explode();
    }

    protected override void ReturnBullet()
    {
        trailParticle.Stop();
        base.ReturnBullet();
    }

    void Explode() {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, explosionRadius, targetLayer);
        foreach (var target in targets){
            target.GetComponent<BaseMonsterController>().OnHit(Damage, false);
        }
        ReturnBullet();
    }
}
