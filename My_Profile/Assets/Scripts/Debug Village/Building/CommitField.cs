using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CommitField : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Sprite[] filedSprite;
    [SerializeField] string date;
    public bool isCommited = false;
    [SerializeField] TextMeshPro dateText;
    public void InitFiled(bool commit, string commitDate) {
        date = commitDate;
        dateText.text = date;
        isCommited = commit;
        if (commit)
            sprite.sprite = filedSprite[1];
        else
            sprite.sprite = filedSprite[0];
    }
}
