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
    [SerializeField] int maxUnit;
    [SerializeField] int curUnit;
    [SerializeField] int curPhase;

    [SerializeField] Queue<BaseMonsterController> monsterPool = new Queue<BaseMonsterController>();
    [SerializeField] Queue<Bullet> bulletPool = new Queue<Bullet>();

    private void Awake(){
        PreparePool();
    }

    public void PreparePool() {
        InitPool();
        sceneDirector.SceneReady();
        StartCoroutine("SpawnRoutine");
    }

    //Pool에 최대 유닛 수 만큼 몬스터 넣기
    public void InitPool() {
        for (int i = curUnit; i < maxUnit; i++) {
            GameObject monsterObj = Instantiate(monsterPefabs[0], this.transform);
            BaseMonsterController monster = monsterObj.GetComponent<BaseMonsterController>();
            monster.PreSetUp();
            monsterObj.SetActive(false);
            monsterPool.Enqueue(monster);
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
        monsterPool.Enqueue(monster);
        curUnit--;
    }

    public void InsertUsedBullet(Bullet bullet) {
        bullet.gameObject.SetActive(false);
        bulletPool.Enqueue(bullet);
    }

    IEnumerator SpawnRoutine() {
        yield return new WaitForSeconds(1);
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
