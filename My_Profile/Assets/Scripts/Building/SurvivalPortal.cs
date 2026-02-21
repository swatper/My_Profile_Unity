using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SurvivalPortal : KeyHintDisplay
{
    protected override void OnInteract(Define.KeyEvent keyEvent)
    {
        if (keyEvent == Define.KeyEvent.Enter) {
            //TODO: æ¿ ¿Ãµø
            Debug.Log("∞‘¿”æ¿¿∏∑Œ ¿Ãµø");
        }
    }
}
