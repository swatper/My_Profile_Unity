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
    [SerializeField] Queue<Bullet> bulletPool = new Queue<Bullet>();

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

    /// <summary>
    /// 맨 처음 한번만 실행
    /// </summary>
    public void PreparePool() {
        sceneDirector.SceneReady();
        StartCoroutine("SpawnRoutine");
    }

    //Pool에 최대 유닛 수 만큼 몬스터 넣기
    public void InitPool() {
        for (int i = curUnit; i < paseState.MaxUnit; i++) {
            CreateNewMonster();
        }
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

    //Pool에서 총알 소환하기
    public Bullet GetBulletFromPool(int index) { 
        Bullet bullet = null;

        if (bulletPool.Count  > 0) {
            bullet = bulletPool.Dequeue();
        }
        else
        {
            GameObject bulletobj = Instantiate(bulletPefabs[index], this.transform);
            bullet = bulletobj.GetComponent<Bullet>();
        }
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

    public void InsertUsedBullet(Bullet bullet) {
        bullet.gameObject.SetActive(false);
        bulletPool.Enqueue(bullet);
    }

    IEnumerator SpawnRoutine() {
        yield return new WaitForSeconds(0.7f);
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            SpawnFromPool();
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
