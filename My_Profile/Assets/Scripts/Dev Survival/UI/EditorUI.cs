using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//#if UNITY_EDITOR
public class EditorUI : MonoBehaviour
{
    [SerializeField] WeaponHandler wHandler;

    public void UpgradeWeapon(int wID) {
        if(wHandler == null)
            wHandler = GameManager.Player.GetComponent<WeaponHandler>();

        wHandler.UpgradeWeapon(wID);
    }
}
//#endif