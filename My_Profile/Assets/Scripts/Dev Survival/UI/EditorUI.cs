using System;
using System.Collections;
using UnityEngine;

//#if UNITY_EDITOR
/// <summary>
/// 추후 막을 예정
/// </summary>
public class EditorUI : MonoBehaviour
{
    [SerializeField] WeaponHandler wHandler;
    [SerializeField] TerrainScaler terrain;

    private void Start(){
        StartCoroutine("GetWeapon");
    }

    /// <summary>
    /// 버튼용
    /// </summary>
    /// <param name="wID"></param>
    public void UpgradeWeaponToButton(int wID) {
        if (wID < 4)
            wHandler.WeaponUpgradeButton(wID);
        else if (wID == 6)
            terrain.Upgrade();
    }

    IEnumerator GetWeapon() {
        while (wHandler == null) {
            wHandler = GameManager.Player.GetComponent<WeaponHandler>();
            yield return null;
        }
    }
}
//#endif