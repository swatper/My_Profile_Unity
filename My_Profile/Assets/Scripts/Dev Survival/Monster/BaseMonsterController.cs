using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMonsterController : MonoBehaviour
{
    [Header("MonsterInfo")]
    [Tooltip("몬스터 능력치")]
    [SerializeField] MonsterData mData;
    [SerializeField] MonsterState mState;
    [Tooltip("플레이어")]
    [SerializeField] Rigidbody2D target;
    [Header("Monster Componen")]
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] SpriteRenderer mSprite;

    public void PreSetUp() {
        rigid = GetComponent<Rigidbody2D>();
        mSprite = GetComponent<SpriteRenderer>();
        target = GameManager.Player.GetComponent<Rigidbody2D>();
        InitData(mData);
    }

    /// <summary>
    /// Pool에서 꺼내질 때 마다 실행 (능력치 초기화)
    /// </summary>
    /// <param name="data"></param>
    public void InitData(MonsterData data) {
        mData = data;
        SetMonsterData(data);
    }

    void SetMonsterData(MonsterData newData) {
        mState = new MonsterState(newData);
    }

    private void FixedUpdate()
    {
        if (mState.isDead)
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
}
