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
            playerController.ReadUIInfo();
            Debug.Log("게임씬으로 이동");
            GameManager.Instance.StopTimer();
            GameManager.SceneLoader.LoadScene("Dev Survival");
        }
    }
    protected override void OnDetected(Collider2D collision)
    {
        playerController = collision.transform.parent.GetComponent<PlayerController>();
    }

    protected override void OnLost(Collider2D other)
    {
        playerController = null;
    }
}
