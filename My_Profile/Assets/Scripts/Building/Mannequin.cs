using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mannequin : KeyHintDisplay
{
    [SerializeField] SpriteRenderer sprite;
    Color32 offColor = new Color32(142,142, 142, 255);
    Color32 OnColor = new Color32(255, 255, 255, 255);
    [SerializeField] PlayerController playerController;
    [SerializeField] PlayerData skinStat;

    protected override void OnDetected(Collider2D collision) {
        sprite.color = OnColor;
        playerController = collision.transform.parent.GetComponent<PlayerController>();
    }

    protected override void OnLost(Collider2D other)
    {
        sprite.color = offColor;
        playerController = null;
    }

    protected override void OnInteract(Define.KeyEvent keyEvent)
    {
        if (keyEvent == Define.KeyEvent.Tab && playerController != null) {
            playerController.ChangeStat(skinStat);
        }
    }
}
