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

    [SerializeField] Queue<BaseMonsterController> readyPool = new Queue<BaseMonsterController>();

    private void Awake(){
        PreparePool();
    }

    public void PreparePool() {
        InitPool();
        sceneDirector.SceneReady();
        StartCoroutine("SpawnRoutine");
    }

    //Pool에 최대 유닛 수 만큼 채워 넣기
    public void InitPool() {
        for (int i = curUnit; i < maxUnit; i++) {
            GameObject monsterObj = Instantiate(monsterPefabs[0], this.transform);
            BaseMonsterController monster = monsterObj.GetComponent<BaseMonsterController>();
            monsterObj.SetActive(false);
            readyPool.Enqueue(monster);
        }
    }

    //Pool에서 소환히기
    public void SpawnFromPool() {
        if (readyPool.Count > 0) {
            BaseMonsterController monster = readyPool.Dequeue();
            monster.gameObject.SetActive(true);
            curUnit++;
        }
    }

    public void InsertDeadMonster(BaseMonsterController monster) {
        readyPool.Enqueue(monster);
        curUnit--;
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
