using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMP : MonoBehaviour
{

    private void Awake()
    {
        GameManager.SceneLoader.SceneReady();
    }
}
