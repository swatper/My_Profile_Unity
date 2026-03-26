using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] GameObject weaponContainer;
    [SerializeField] List<string> weaponPaths=new List<string> {
        "Weapon/CppCrossbow",
        "Weapon/CSharpStaff",
        "Weapon/FlutterWidget"
    };
    [SerializeField] List<BaseWeapon> weaponList = new List<BaseWeapon>();
    [SerializeField] Transform pPivot;
    private void Awake(){
        InitContainer();
        PreLoadWeapons();
        UpgradeWeapon(0);
    }

    void InitContainer()
    {
        pPivot = transform.Find("PlayerPivot");
        if (weaponContainer == null)
        {
            weaponContainer = new GameObject("Weapon Container");
            weaponContainer.transform.SetParent(pPivot);
            weaponContainer.transform.localPosition = Vector3.zero;
            weaponContainer.transform.localRotation = Quaternion.identity;
        }
    }

    void PreLoadWeapons() {
        foreach (string path in weaponPaths) {
            GameObject go = GameManager.Resource.Instantiate(path, weaponContainer.transform, false);
            if (go != null) {
                weaponList.Add(go.GetComponent<BaseWeapon>());
            }
        }
    }

    /// <summary>
    /// 무기 활성화 or 업그레이드
    /// </summary>
    /// <param name="wID"></param>
    public void UpgradeWeapon(Define.UpgradeType type){
        BaseWeapon target = GetWeaponScript(type);
        if (target.isUnlocked)
            target.LevelUp();
        else {
            target.isUnlocked = true;
            target.gameObject.SetActive(true);
        }
    }

    public string GetUpgradeInfo(Define.UpgradeType type) {
        return GetWeaponScript(type).UpgradeInfo();
    }

    public bool CheckUpgradeable(Define.UpgradeType type) {
        return GetWeaponScript(type).isMaxLevel;
    }

    public bool CheckUnlock(Define.UpgradeType type) {
        return GetWeaponScript(type).isUnlocked;
    }

    BaseWeapon GetWeaponScript(Define.UpgradeType type){
        return weaponList[(int)type];
    }

    private void OnDestroy(){
        Destroy(weaponContainer);
    }

//#if UNITY_EDITOR
    public void WeaponUpgradeButton(int wID) {
        BaseWeapon targret = weaponList[wID];
        if (targret.isUnlocked)
            targret.LevelUp();
        else
        {
            targret.isUnlocked = true;
            targret.gameObject.SetActive(true);
        }
    }
//#endif
}
