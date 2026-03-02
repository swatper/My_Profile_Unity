using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SurvivalPortal : KeyHintDisplay
{
    [SerializeField] PlayerController playerController;
    protected override void OnInteract(Define.KeyEvent keyEvent)
    {
        if (keyEvent == Define.KeyEvent.Enter) {
            if (GameManager.SceneLoader.isLoading) return;
            GameManager.Instance.StopTimer();
            playerController.ReadUIInfo();
            GameManager.SceneLoader.LoadScene("Dev Survival");
        }
    }
    protected override void OnDetected(Collider2D collision)
    {
        playerController = collision.transform.parent.parent.GetComponent<PlayerController>();
    }

    protected override void OnLost(Collider2D other)
    {
        playerController = null;
    }
}
