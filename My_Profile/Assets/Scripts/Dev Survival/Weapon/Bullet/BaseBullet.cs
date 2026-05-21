using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    public int bID;
    public float Damage;
    int piercingCnt;
    public float lifeTime;
    Rigidbody2D rigid;

    protected virtual void Awake(){
        rigid = GetComponent<Rigidbody2D>();
    }

    public virtual void Init(float damage, int cnt, Vector3 dir = default)
    {
        Damage = damage;
        piercingCnt = cnt;

        gameObject.SetActive(true);
        if (piercingCnt >= 0) {
            rigid.linearVelocity = dir;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    protected virtual void  ReturnBullet() {
        rigid.linearVelocity = Vector2.zero;
        StopAllCoroutines();
        SurvivalSceneDirector.Instance.poolManager.InsertUsedBullet(this);
    }

    public virtual void HitMonster(BaseMonsterController monster) {
        monster.OnHit(Damage);
        if (piercingCnt < 0)
            return;

        piercingCnt--;
        if (piercingCnt < 0){
            ReturnBullet();
        }
    }

    /// <summary>
    /// 투사체 작폭
    /// </summary>
    /// <returns></returns>
    protected IEnumerator DeactivateAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);
        ReturnBullet();
    }
}
