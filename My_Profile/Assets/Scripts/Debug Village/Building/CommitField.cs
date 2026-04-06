using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommitField : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Sprite[] filedSprite;
    [SerializeField] string date;
    public bool isCommited = false;
    public void InitFiled(bool commit, string commitDate) {
        date = commitDate;
        isCommited = commit;
        if (commit)
            sprite.sprite = filedSprite[1];
        else
            sprite.sprite = filedSprite[0];
    }
}
