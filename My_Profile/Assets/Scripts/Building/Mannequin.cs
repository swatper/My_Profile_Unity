using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mannequin : KeyHintDisplay
{
    public int skinType;
    [SerializeField] SpriteRenderer sprite;
    Color32 offColor = new Color32(142,142, 142, 255);
    Color32 OnColor = new Color32(255, 255, 255, 255);
    [SerializeField] PlayerController playerController;

    private void Awake(){
        GameManager.Input.SubscribeKeyEvent(ChangeSkin);
    }

    protected override void OnDetected(Collider2D collision) {
        sprite.color = OnColor;
        playerController = collision.transform.parent.GetComponent<PlayerController>();
    }

    protected override void OnLost(Collider2D other)
    {
        sprite.color = offColor;
        playerController = null;
    }

    void ChangeSkin(Define.KeyEvent keyEvent) {
        if (keyEvent == Define.KeyEvent.Tab && playerController!= null) 
            playerController.ChangePlayerSkin(skinType);
    }
}
