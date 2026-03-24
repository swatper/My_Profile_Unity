using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UpgradeSlot : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text desc;
    [SerializeField] Define.UpgradeType type;
    [SerializeField] WeaponHandler weaponHandler;
    [SerializeField] PlayerController playerController;

    //UI 업데이트 용
    public void InitSlot(UpgradeType type) {
        this.type = type;
        //TODO: 해당 타입에 맞게 데이터를 UII에 할당하기

    }

    /// <summary>
    /// 버튼이 눌렸을 때 호출될 메서드
    /// </summary>
    public void OnClickUpgrade()
    {
        int id = (int)type;

        //CrossBow ~ Unity (무기군)
        if (id < 4)
        {
            GameManager.Player.GetComponent<WeaponHandler>().UpgradeWeapon(type);
        }
        //Hp, Speed (능력치군)
        else
        {
            GameManager.Player.GetComponent<PlayerController>().UpgradeStat(type);
        }

        SurvivalSceneDirector.Instance.Resume();
    }
}
