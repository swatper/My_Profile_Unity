using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage;
    public int piercingCnt;
    public float lifeTime;
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int cnt, Vector3 dir)
    {
        Damage = damage;
        piercingCnt = cnt;
        gameObject.SetActive(true);
        if (piercingCnt > 0) {
            rigid.velocity = dir;
        }
        StartCoroutine("DeactivateAfterTime");
    }

    public void DescPiercingCNT()
    { 
        piercingCnt--;
        if (piercingCnt < 0) {
            rigid.velocity = Vector2.zero;
            StopAllCoroutines();
            SurvivalSceneDirector.Instance.poolManager.InsertUsedBullet(this);
        }
    }
    IEnumerator DeactivateAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);

        //시간이 다 되면 속도를 멈추고 풀에 반납
        rigid.velocity = Vector2.zero;
        SurvivalSceneDirector.Instance.poolManager.InsertUsedBullet(this);
    }
}
