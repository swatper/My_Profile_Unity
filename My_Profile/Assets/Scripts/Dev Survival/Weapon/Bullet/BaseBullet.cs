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

    public virtual void Init(float damage, int cnt, Vector3 dir)
    {
        Damage = damage;
        piercingCnt = cnt;
        StopAllCoroutines();

        gameObject.SetActive(true);
        if (piercingCnt >= 0) {
            rigid.velocity = dir;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        StartCoroutine("DeactivateAfterTime");
    }

    protected virtual void  ReturnBullet() {
        rigid.velocity = Vector2.zero;
        StopAllCoroutines();
        SurvivalSceneDirector.Instance.poolManager.InsertUsedBullet(this);
    }

    public void DescPiercingCNT()
    { 
        piercingCnt--;
        if (piercingCnt < 0) {
            ReturnBullet();
        }
    }

    /// <summary>
    /// Åõ»çÃ¼ ÀÚÆø
    /// </summary>
    /// <returns></returns>
    IEnumerator DeactivateAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);
        ReturnBullet();
    }
}
