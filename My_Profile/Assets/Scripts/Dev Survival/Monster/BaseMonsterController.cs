using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMonsterController : MonoBehaviour
{
    [Header("MonsterInfo")]
    [Tooltip("몬스터 기본 능력치")]
    [SerializeField] MonsterData mData;
    [SerializeField] MonsterState mState;
    [Tooltip("플레이어")]
    [SerializeField] Rigidbody2D target;
    [Header("Monster Componen")]
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] Collider2D mColl;
    [SerializeField] SpriteRenderer mSprite;
    [SerializeField] Animator mAnimator;

    public void PreSetUp() {
        rigid = GetComponent<Rigidbody2D>();
        mColl = GetComponent<Collider2D>();
        mSprite = GetComponent<SpriteRenderer>();
        target = GameManager.Player.GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
        InitMonster(mData);
    }

    /// <summary>
    /// Pool에서 꺼내질 때 마다 실행 (능력치/상태 초기화)
    /// </summary>
    /// <param name="data"></param>
    public void InitMonster(MonsterData data) {
        mData = data;
        SetMonsterData(data);
        SetComponentState(mState.isDead);
    }

    void SetMonsterData(MonsterData newData) {
        mState = new MonsterState(newData);
    }

    void SetComponentState(bool IsDead) {
        mState.isDead = IsDead;
        mColl.enabled = !IsDead;
        rigid.simulated = !IsDead;
        mAnimator.SetBool("Dead", IsDead);
        mSprite.sortingOrder = IsDead ? 0 : 1;
        gameObject.SetActive(!IsDead);
    }

    public void Dead() {
        SurvivalSceneDirector.Instance.poolManager.InsertDeadMonster(this);
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (mState.isDead || mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        //플레이어 방향 찾기
        Vector2 dirVec = target.position - rigid.position;
        //가야할 위치 찾기
        Vector2 nextVec = dirVec.normalized * mState.curSpeed * Time.fixedDeltaTime;
        //이동
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        mSprite.flipX = target.position.x < rigid.position.x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Weapon"))
            return;

        mState.curHp -= collision.GetComponent<Bullet>().Damage;
        collision.GetComponent<Bullet>().DescPiercingCNT();

        StartCoroutine("KnockBack");

        if (mState.curHp > 0)
        {
            mAnimator.SetTrigger("Hit");
        }
        else {
            mState.isDead = true;
            SetComponentState(mState.isDead);
        }
    }

    IEnumerator KnockBack() { 
        yield return null;
        Vector3 playerPos = GameManager.Player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }
}
