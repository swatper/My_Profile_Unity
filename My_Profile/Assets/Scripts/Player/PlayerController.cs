using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Input.SubscribeKeyEvent(PlayerControll);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayerControll(Define.KeyEvent key) {
        switch (key) {
            case Define.KeyEvent.Left:
                break;
            case Define.KeyEvent.Right:
                break;
            case Define.KeyEvent.Up:
                break;
            case Define.KeyEvent.Down:
                break;
        }
    }
}
