using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UpgradeSlot : MonoBehaviour
{
    [Header("업그레이드 할 대상 정보")]
    [SerializeField] Text targetName;
    [SerializeField] Text targetDesc;
    [SerializeField] UpgradeType targetType;
    int upID;
    [SerializeField] Image icon;
    [SerializeField] Sprite defaultIcon;
    [SerializeField] GameObject[] fileNames;
    [SerializeField] IUpgradable upgradeTarget;
    [SerializeField] WeaponHandler wHandler;

    /// <summary>
    /// 슬롯에 업그레이드 할 대상 입력
    /// </summary>
    /// <param name="type"></param>
    /// <returns>업그레이드 가능 여부</returns>
    public void InitSlot(UpgradeType type, IUpgradable target) {
        targetType = type;
        upgradeTarget = target;

        upID = (int)targetType;

        if (upID < 4){
            SetWeaponInfo();
        }
        else
            SetStatInfo();

        targetName.text = $"//{targetType.ToString()}";

        if (target.GetIcon() != null)
            icon.sprite = target.GetIcon();
        else
            icon.sprite = defaultIcon;
    }

    void SetWeaponInfo() {
        InitFileName(upgradeTarget.GetUnlockState());
         targetDesc.text = upgradeTarget.GetDescription();
    }

    void SetStatInfo() {
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
        upgradeTarget.Upgrade();
        SurvivalSceneDirector.Instance.Resume();
    }
}
