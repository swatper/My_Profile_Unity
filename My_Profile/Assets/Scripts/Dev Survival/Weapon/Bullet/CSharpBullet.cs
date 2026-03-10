using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSharpBullet : BaseBullet
{
    [SerializeField] ParticleSystem trailParticle;

    protected override void Awake()
    {
        base.Awake();
        trailParticle = GetComponentInChildren<ParticleSystem>();
    }

    public override void Init(float damage, int cnt, Vector3 dir){
        trailParticle.Clear();
        trailParticle.Play();
        base.Init(damage, cnt, dir);
    }

    protected override void ReturnBullet()
    {
        trailParticle.Stop();
        base.ReturnBullet();
    }
}
