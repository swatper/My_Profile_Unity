using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
public class EditorUI : MonoBehaviour
{
    [SerializeField] WeaponHandler wHandler;

    private void Start(){
        StartCoroutine("GetWeapon");
    }

    /// <summary>
    /// 버튼용
    /// </summary>
    /// <param name="wID"></param>
    public void UpgradeWeaponToButton(int wID) {
        wHandler.WeaponUpgradeButton(wID);
    }

    IEnumerator GetWeapon() {
        while (wHandler == null) {
            Debug.Log("무기 찾는 중");
            wHandler = GameManager.Player.GetComponent<WeaponHandler>();
            yield return null;
        }
    }

}
#endif