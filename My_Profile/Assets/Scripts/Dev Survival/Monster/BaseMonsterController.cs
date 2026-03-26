using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMonsterController : MonoBehaviour
{
    [Header("MonsterInfo")]
    [Tooltip("Monster Data")]
    [SerializeField] MonsterData mData;
    [Tooltip("Current Monster Data")]
    [SerializeField] MonsterStat mStat;
    [SerializeField] int curLevel;
    [Tooltip("플레이어")]
    [SerializeField] Rigidbody2D target;
    [Header("Monster Component")]
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] Collider2D mColl;
    [SerializeField] SpriteRenderer mSprite;
    [SerializeField] Animator mAnimator;

    /// <summary>
    /// PoolManager가 진행
    /// </summary>
    /// <param name="monLevel"></param>
    public void PreSetUp(int monLevel) {
        curLevel = monLevel;
        rigid = GetComponent<Rigidbody2D>();
        mColl = GetComponent<Collider2D>();
        mSprite = GetComponent<SpriteRenderer>();
        target = GameManager.Player.GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
        InitMonster();
    }

    /// <summary>
    /// Pool에서 꺼내질 때 마다 실행 (능력치/상태 초기화)
    /// </summary>
    /// <param name="data"></param>
    public void InitMonster() {
        SetMonsterData();
        SetComponentState(mStat.isDead);
    }

    void SetMonsterData() {
        mStat = mData.levelTables[curLevel - 1];
    }

    void SetComponentState(bool IsDead) {
        mStat.isDead = IsDead;
        mColl.enabled = !IsDead;
        rigid.simulated = !IsDead;
        mAnimator.SetBool("Dead", IsDead);
        mSprite.sortingOrder = IsDead ? 0 : 1;
    }

    public int GetMonsterID() {
        return mData.MonsterID;
    }

    public int GetMonserLevel() {
        return curLevel;
    }

    public void Dead() {
        SurvivalSceneDirector.Instance.OnMonsterKilled?.Invoke(mStat.Exp);
        SurvivalSceneDirector.Instance.poolManager.InsertDeadMonster(this);
    }

    private void FixedUpdate()
    {
        if (mStat.isDead || mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        //플레이어 방향 찾기
        Vector2 dirVec = target.position - rigid.position;
        //가야할 위치 찾기
        Vector2 nextVec = dirVec.normalized * mStat.curSpeed * Time.fixedDeltaTime;
        //이동
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate(){
        mSprite.flipX = target.position.x < rigid.position.x;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (!collision.CompareTag("Weapon"))
            return;
        collision.GetComponent<BaseBullet>().HitMonster(this);
    }

    public void OnHit(float damage, bool needKnockBack = true)
    {
        mStat.curHp -= damage;
        if (needKnockBack) 
            StartCoroutine("KnockBack");
        if (mStat.curHp > 0)
            mAnimator.SetTrigger("Hit");
        else{
            mStat.isDead = true;
            SetComponentState(mStat.isDead);
        }
    }

    IEnumerator KnockBack() { 
        yield return null;
        Vector3 playerPos = GameManager.Player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * mStat.KnockBack, ForceMode2D.Impulse);
    }
}
