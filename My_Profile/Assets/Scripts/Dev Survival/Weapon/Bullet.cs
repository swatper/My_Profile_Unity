using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage;
    public int piercingCnt;

    public void Init(int damage, int cnt)
    {
        Damage = damage;
        piercingCnt = cnt;
        gameObject.SetActive(true);
    }

    public void DescPiercingCNT() => piercingCnt--;

    private void FixedUpdate()
    {
        if (piercingCnt < 0)
            gameObject.SetActive(false);
    }
}
