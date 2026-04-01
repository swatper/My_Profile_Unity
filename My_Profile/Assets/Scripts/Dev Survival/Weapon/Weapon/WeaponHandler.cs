using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class WeaponHandler : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] GameObject weaponContainer;
    [SerializeField] List<string> weaponPaths=new List<string> {
        "Weapon/CppCrossbow",
        "Weapon/CSharpStaff",
        "Weapon/FlutterWidget",
        "Weapon/UnityBook"
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
    public void UpgradeWeapon(UpgradeType type){
        BaseWeapon target = GetWeaponScript(type);
        if (target.isUnlocked)
            target.Upgrade();
        else {
            target.isUnlocked = true;
            target.gameObject.SetActive(true);
        }
    }

    public BaseWeapon GetWeaponScript(UpgradeType type){
        return weaponList[(int)type];
    }

    public void UnlockWeapon(UpgradeType type) {
        Debug.Log($"새 무기 활성화: {type.ToString()}");
        weaponList[(int)type].gameObject.SetActive(true);
    }

    private void OnDestroy(){
        Destroy(weaponContainer);
    }

    //#if UNITY_EDITOR
    public void WeaponUpgradeButton(int wID) {
        BaseWeapon targret = weaponList[wID];
        if (targret.isUnlocked)
            targret.Upgrade();
        else
        {
            targret.isUnlocked = true;
            targret.gameObject.SetActive(true);
        }
    }
//#endif
}
