using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Define;

public class Mannequin : KeyHintDisplay
{
    [SerializeField] SpriteRenderer sprite;
    Color32 offColor = new Color32(142,142, 142, 255);
    Color32 OnColor = new Color32(255, 255, 255, 255);
    [SerializeField] PlayerController playerController;
    [SerializeField] PlayerData skinStat;

    protected override void OnDetected(Collider2D collision) {
        sprite.color = OnColor;
        playerController = collision.transform.parent.parent.GetComponent<PlayerController>();
    }

    protected override void OnLost(Collider2D other)
    {
        sprite.color = offColor;
        playerController = null;
    }

    protected override void OnInteract(KeyEvent keyEvent)
    {
        if ((keyEvent == KeyEvent.Tab || keyEvent == KeyEvent.Enter) && playerController != null) {
            playerController.ChangeStat(skinStat);
        }
    }
}
