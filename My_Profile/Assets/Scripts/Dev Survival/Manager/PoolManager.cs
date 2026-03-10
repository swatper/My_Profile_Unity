using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] BaseSceneDirector  sceneDirector;
    [SerializeField] GameObject[] monsterPefabs;
    [SerializeField] GameObject[] bulletPefabs;
    [Header("Pool Settings")]
    [SerializeField] float spawnRate = 0.5f;
    [SerializeField] PaseData spawnData;
    [SerializeField] Pase paseState;
    [SerializeField] int curPase;
    [SerializeField] int curUnit;

    [SerializeField] Queue<BaseMonsterController> monsterPool = new Queue<BaseMonsterController>();
    [Header("각 투사체 Pool")]
    [SerializeField] List<Queue<BaseBullet>> bulletPools = new List<Queue<BaseBullet>>();
    private void Awake(){
        curPase = -1;
        PaseUp();
        PreparePool();
    }

    public void PaseUp() {
        curPase++;
        paseState = spawnData.levelTables[curPase];
        //대기하고 있는 전 단계 몬스터 제거
        while (monsterPool.Count > 0){
            BaseMonsterController oldMonster = monsterPool.Dequeue();
            Destroy(oldMonster.gameObject);
        }
        InitPool();
    }

    //Pool에 최대 유닛 수 만큼 몬스터 넣기
    public void InitPool()
    {
        //몬스터 Pool 준비
        for (int i = curUnit; i < paseState.MaxUnit; i++){
            CreateNewMonster();
        }
        //투사체 Pool 준비
        for (int i = 0; i < bulletPefabs.Length; i++){
            bulletPools.Add(new Queue<BaseBullet>());
        }
    }

    /// <summary>
    /// 맨 처음 한번만 실행
    /// </summary>
    public void PreparePool() {
        sceneDirector.SceneReady();
        StartCoroutine("SpawnRoutine");
    }


    //Pool에서 몬스터 소환히기
    public void SpawnFromPool() {
        if (monsterPool.Count > 0) {
            BaseMonsterController monster = monsterPool.Dequeue();
            monster.InitMonster();
            monster.gameObject.SetActive(true);
            curUnit++;
        }
    }

    void CreateNewMonster() {
        GameObject monsterObj = Instantiate(monsterPefabs[curPase], this.transform);
        BaseMonsterController newMonster = monsterObj.GetComponent<BaseMonsterController>();
        newMonster.PreSetUp();
        monsterObj.SetActive(false);
        monsterPool.Enqueue(newMonster);
    }

    /// <summary>
    /// Pool에서 총알 달라고 요청
    /// </summary>
    /// <param name="index">0: C++ 1: C#</param>
    /// <returns></returns>
    public BaseBullet GetBulletFromPool(int index) {
        BaseBullet bullet = null;

        if (bulletPools[index].Count > 0){
            bullet = bulletPools[index].Dequeue();
            bullet.gameObject.SetActive(true);
            return bullet;
        }

        GameObject bulletobj = Instantiate(bulletPefabs[index], this.transform);
        bullet = bulletobj.GetComponent<BaseBullet>();
        bullet.bID = index;

        return bullet;
    }

    public void InsertDeadMonster(BaseMonsterController monster) {
        monster.gameObject.SetActive(false);
        curUnit--;

        //필터링: 이전 몬스터 관리
        if (monster.GetMonsterID() == curPase)
            monsterPool.Enqueue(monster);
        else {
            Destroy(monster.gameObject);
            CreateNewMonster();
        }
    }

    public void InsertUsedBullet(BaseBullet bullet) {
        bullet.gameObject.SetActive(false);
        bulletPools[bullet.bID].Enqueue(bullet);
    }

    IEnumerator SpawnRoutine() {
        yield return new WaitForSeconds(0.7f);
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            SpawnFromPool();
        }
    }

    private void OnDestroy(){
        StopAllCoroutines();
    }
}
