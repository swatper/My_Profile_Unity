using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mannequin : MonoBehaviour
{
    public int skinType;
    [SerializeField] SpriteRenderer sprite;
    Color32 offColor = new Color32(142,142, 142, 255);
    Color32 OnColor = new Color32(255, 255, 255, 255);
    [SerializeField] Animator tabAni;
    [SerializeField] PlayerController playerController;

    private void Awake()
    {
        GameManager.Input.SubscribeKeyEvent(ChangeSkin);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "ETC")
            return;

        sprite.color = OnColor;
        playerController = collision.transform.parent.GetComponent<PlayerController>();
        tabAni.Play("Tab");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != "ETC")
            return;

        sprite.color = offColor;
        playerController = null;
        tabAni.Play("Default");
    }

    void ChangeSkin(Define.KeyEvent keyEvent) {
        if (keyEvent == Define.KeyEvent.Tab && playerController!= null) 
            playerController.ChangePlayerSkin(skinType);
    }
}
