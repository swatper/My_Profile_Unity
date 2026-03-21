using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlot : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text desc;
    [SerializeField] Define.UpgradeType type;

    // 버튼이 눌렸을 때 호출될 메서드
    public void OnClickUpgrade()
    {
        int id = (int)type;

        //CrossBow ~ Unity (무기군)
        if (id < 4){
            GameManager.Player.GetComponent<WeaponHandler>().UpgradeWeapon(type);
        }
        //Hp, Speed (능력치군)
        else{
            GameManager.Player.GetComponent<PlayerController>().UpgradeStat(type);
        }

        SurvivalSceneDirector.Instance.Resume();
    }
}
