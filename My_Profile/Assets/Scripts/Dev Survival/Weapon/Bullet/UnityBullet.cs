using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityBullet : BaseBullet
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HitMonster(collision.GetComponent<BaseMonsterController>());
    }
}