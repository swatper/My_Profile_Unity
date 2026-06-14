using UnityEngine;

public class UnityBullet : BaseBullet
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HitMonster(collision.GetComponent<BaseMonsterController>());
    }
}