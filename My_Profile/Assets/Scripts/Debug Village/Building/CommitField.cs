using UnityEngine;
using TMPro;

public class CommitField : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Sprite[] filedSprite;
    [SerializeField] TextMeshPro dateText;
    [Header("한국 시간 기준")]
    public bool isCommited = false;
    [SerializeField] string date;
    [SerializeField] string time;

    private void Reset(){
        sprite = gameObject.GetComponent<SpriteRenderer>();
        dateText = gameObject.GetComponentInChildren<TextMeshPro>();
    }

    public void InitFiled(bool commit, string commitDate, string commitTime = "00:00:00") {
        date = commitDate;
        time = commitTime;
        dateText.text = date;
        isCommited = commit;
        if (commit)
            sprite.sprite = filedSprite[1];
        else
            sprite.sprite = filedSprite[0];
    }
}
