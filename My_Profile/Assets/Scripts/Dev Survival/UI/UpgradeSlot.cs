using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UpgradeSlot : MonoBehaviour
{
    [Header("업그레이드 할 대상 정보")]
    [SerializeField] Text targetName;
    [SerializeField] Text targetDesc;
    [SerializeField] UpgradeType targetType;
    int uID;
    [SerializeField] Image icon;
    [SerializeField] GameObject[] fileNames;
    [SerializeField] WeaponHandler weaponHandler;
    [SerializeField] PlayerController playerController;

    /// <summary>
    /// 슬롯에 업그레이드 할 대상 입력
    /// </summary>
    /// <param name="type"></param>
    /// <returns>업그레이드 가능 여부</returns>
    public bool InitSlot(UpgradeType type) {
        SetUpgradeTarget();

        targetType = type;
        uID = (int)targetType;
        Debug.Log($"{gameObject.name}: {uID}");
        //무기 관련
        if (uID < 2)
        {
            if (weaponHandler.CheckUpgradeable(targetType))
                return false;
            SetWeaponInfo();
        }
        else
            SetStatInfo();

        targetName.text = $"//{targetType.ToString()}";
        //icon.sprite =
        return true;
    }

    void SetUpgradeTarget() {
        weaponHandler = GameManager.Player.GetComponent<WeaponHandler>();
        playerController = GameManager.Player.GetComponent<PlayerController>();
    }

    void SetWeaponInfo() {
        InitFileName(!weaponHandler.CheckUnlock(targetType));
         targetDesc.text = weaponHandler.GetUpgradeInfo(targetType);
    }
    void SetStatInfo() {
        Debug.Log("능력치 강화");
        InitFileName(false);
        targetDesc.text = "    Stat ++ ; \n" + "}";
    }


    void InitFileName(bool isNew){
        for (int i = 0; i < fileNames.Length; i++)
            fileNames[i].SetActive(false);

        if (isNew)
            fileNames[1].SetActive(true);
        else
            fileNames[0].SetActive(true);
    }


    /// <summary>
    /// 버튼이 눌렸을 때 호출될 메서드
    /// </summary>
    public void OnClickUpgrade()
    {
        //CrossBow ~ Unity (무기군)
        if (uID < 2)
            weaponHandler.UpgradeWeapon(targetType);
        //Hp, Speed (능력치군)
        else
            Debug.Log("능력치 강화 예정");
            //playerController.GetComponent<PlayerController>().UpgradeStat(targetType);
        SurvivalSceneDirector.Instance.Resume();
    }
}
