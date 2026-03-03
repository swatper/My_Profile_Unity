using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScene : MonoBehaviour
{
    void Start()
    {
        GameManager.Player.InitPlayerPositionInVillagel();
        GameManager.SceneLoader.SceneReady();
    }


}
