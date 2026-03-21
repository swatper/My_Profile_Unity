using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SurvivalSceneDirector : BaseSceneDirector
{
    public static SurvivalSceneDirector Instance { get; private set; }
    public PoolManager poolManager;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [Header("Exp Info")]
    [SerializeField] EXPUI expBar;
    [SerializeField] ExpData expData;
    [SerializeField] Exp reqExp;
    [SerializeField] float curExp;
    [SerializeField] int curLv;
    public Action<float> OnMonsterKilled;
    [Header("Upgrade Slot")]
    [SerializeField] GameObject solt;

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
        virtualCamera.Follow = GameManager.Player.transform;
        //무기 관리 스크립트 추가
        if (GameManager.Player.GetComponent<WeaponHandler>() == null)
            GameManager.Player.gameObject.AddComponent<WeaponHandler>();
        poolManager = FindObjectOfType<PoolManager>();
        GameManager.Player.InitPlayerInSurvival();
        GameManager.SceneLoader.SceneReady();
    }
    public override void GoToScene()
    {
        //무기 관리 스크립트 제거
        WeaponHandler handler = GameManager.Player.GetComponent<WeaponHandler>();
        if (handler != null)
            Destroy(handler);
        base.GoToScene();
    }

    public void PaseUp(){
        poolManager.PaseUp();
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

    public void Puase(){
        GameManager.Player.ReadUIInfo();
        solt.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume() {
        GameManager.Player.StopReadUIInfo();
        solt.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
