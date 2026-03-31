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
        GameManager.SceneLoader.SceneReady();
    }
    public override void GoToScene()
    {
        //무기 관리 스크립트 제거
        if (wHandler != null)
            Destroy(wHandler);
        base.GoToScene();
    }

    public void PaseUp(){
        poolManager.PaseUp();
    }
    public void Puase()
    {
        GameManager.Player.ReadUIInfo();
        solt.SetActive(true);
        Time.timeScale = 0f;
        InitSlots();
    }

    public void Resume()
    {
        GameManager.Player.StopReadUIInfo();
        solt.SetActive(false);
        Time.timeScale = 1.0f;
    }

    //#if UNITY_EDITOR
    public void PaseDown() {
        poolManager.PaseDown();
    }
//#endif

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
        List<UpgradeType> selectedTypes = new List<UpgradeType>();
        int totalCount = Enum.GetNames(typeof(UpgradeType)).Length;
        PlayerController pController = GameManager.Player;

        //슬롯에 업글 대상 넘겨주기
        for (int i = 0; i < slots.Length; i++){
            bool isSuccess = false;
            UpgradeType randomType;
            while (!isSuccess){
                randomType = (UpgradeType)UnityEngine.Random.Range(0, totalCount);

                //중복 확인
                if (selectedTypes.Contains(randomType))
                    continue;

                //무기인지 능력치인지 확인
                IUpgradable target;
                if ((int)randomType < 4) 
                    target = wHandler.GetWeaponScript(randomType);
                else
                    target = pController;

                //할당 시도(만랩 확인)
                if (slots[i].InitSlot(randomType, target)) {
                    selectedTypes.Add(randomType);
                    isSuccess = true;
                }
                else
                    selectedTypes.Add(randomType);
            }
        }
    }
    #endregion
}
