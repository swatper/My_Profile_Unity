using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Define;

public class SurvivalSceneDirector : BaseSceneDirector
{
    public static SurvivalSceneDirector Instance { get; private set; }
    public PoolManager poolManager;
    [SerializeField] SurvivalSoundController soundController;
    [Header("Exp Info")]
    [SerializeField] EXPUI expBar;
    [SerializeField] ExpData expData;
    [SerializeField] Exp reqExp;
    [SerializeField] float curExp;
    [SerializeField] int curLv;
    public Action<float> OnMonsterKilled;
    [Header("Upgrade Slot")]
    public WeaponHandler wHandler;
    [SerializeField] PlayerController pController;
    [SerializeField] GameObject solt;
    [SerializeField] UpgradeSlot[] slots;

    private void Awake()
    {
        curExp = 0.0f;
        curLv = 1;
        Instance = this;
        SetUpExp();
        OnMonsterKilled += AddExp;
    }
    protected override void InitScene()
    {
        pController = GameManager.Player;
        pController.InitPlayerInSurvival();
        //무기 관리 스크립트 추가
        if (pController.GetComponent<WeaponHandler>() == null)
            wHandler = GameManager.Player.gameObject.AddComponent<WeaponHandler>();
        poolManager = FindObjectOfType<PoolManager>();
        base.InitScene();
        soundController.Init();
        GameManager.SceneLoader.SceneReady();
    }

    public override void GoToScene()
    {
        //무기 관리 스크립트 제거
        if (wHandler != null)
            Destroy(wHandler);
        base.GoToScene();
    }

    public override void MuteSound(){
        soundController.MuteAllSound();
    }

    public override void UnMuteSound(){
        soundController.UnMuteAllSound();
    }

    public void PaseUp(){
        poolManager.PaseUp();
    }

    //#if UNITY_EDITOR
    public void PaseDown(){
        poolManager.PaseDown();
    }
    //#endif

    public void Puase()
    {
        GameManager.Player.ReadUIInfo();
        solt.SetActive(true);
        soundController.PlayLevelUpEffect(0);
        Time.timeScale = 0f;
        InitSlots();
    }

    public void Resume()
    {
        soundController.PlayLevelUpEffect(0);
        GameManager.Player.StopReadUIInfo();
        solt.SetActive(false);
        Time.timeScale = 1.0f;
    }

    #region 경험치용

    /// <summary>
    /// 경험치 바 초기화
    /// </summary>
    public void SetUpExp() {
        reqExp.MaxExp = expData.levelTables[curLv - 1].MaxExp;
        expBar.SetMaxEXP(reqExp.MaxExp);
        expBar.UpdateUI(curExp, curLv);
    }

    /// <summary>
    /// 경험치 추가
    /// </summary>
    /// <param name="exp">경험치 증가량</param>
    void AddExp(float exp) {
        curExp += exp;
        //레벨 업 처리
        while (curExp >= expData.levelTables[curLv - 1].MaxExp) {
            curExp -= expData.levelTables[curLv - 1].MaxExp;

            curLv++;
            OnLevelUp();

            if (curLv -1 < expData.levelTables.Count) {
                SetUpExp();
            }
            else
            {
                //TODO: 만렙 처리
                Debug.Log("만렙");
                break;
            }
        }
        expBar.UpdateUI(curExp, curLv);
    }

    void OnLevelUp() {
        Debug.Log($"레벨업: {curLv}");
        Puase();
    }

    #endregion

    #region 슬롯 초기화용
    void InitSlots() {
        List<UpgradeType> availableUpgrades = new List<UpgradeType>();
        int totalCount = Enum.GetNames(typeof(UpgradeType)).Length;
        PlayerController pController = GameManager.Player;

        //업글 가능한 무기들 미리 준비
        for (int i = 0; i < totalCount; i++){
            UpgradeType type = (UpgradeType)i;
            IUpgradable target;

            if (i < 4)
                target = wHandler.GetWeaponScript(type);
            else
                target = pController;

            if (target.CanUpgrade()){
                availableUpgrades.Add(type);
            }
        }

        //섞기
        for (int i = 0; i < availableUpgrades.Count; i++){
            UpgradeType temp = availableUpgrades[i];
            int randomIndex = UnityEngine.Random.Range(i, availableUpgrades.Count);
            availableUpgrades[i] = availableUpgrades[randomIndex];
            availableUpgrades[randomIndex] = temp;
        }

        //슬롯에 업글 대상 넘겨주기
        for (int i = 0; i < slots.Length; i++){
            if (i >= availableUpgrades.Count){
                slots[i].gameObject.SetActive(false);
                continue;
            }

            UpgradeType selectedType = availableUpgrades[i];
            IUpgradable target = ((int)selectedType < 4) ? wHandler.GetWeaponScript(selectedType) : pController;
            slots[i].gameObject.SetActive(true);
            slots[i].InitSlot(selectedType, target);
        }
    }
    #endregion
}
