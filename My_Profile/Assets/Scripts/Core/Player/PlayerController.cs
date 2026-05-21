using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;
using Core.Define;
public class PlayerController : MonoBehaviour, IUpgradable
{
    public static PlayerController Instance { get; private set; }
    [Header("Player Info")]
    [Tooltip("캐릭터 기본 능력치")]
    [SerializeField] PlayerData pData;
    [Tooltip("실시간 데이터 처리용")]
    [SerializeField] PlayerState pState;
    [Header("Player Component")]
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] private SpriteRenderer pSprite;
    [SerializeField] private Sprite defaultStatIcon;
    [SerializeField] Animator pAnimator;
    [SerializeField] AnimatorOverrideController[] pAniControllers;
    [SerializeField] Transform playerPivot;
    [SerializeField] Transform pPosition;
    public Vector2 inputVec;
    public Vector2 moveBuffer;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            GameManager.Input.SubscribeKeyEvent(OnKeyboardAction);
            pSprite = GetComponent<SpriteRenderer>();
            rigid = GetComponent<Rigidbody2D>();
            pAnimator = GetComponent<Animator>();
            ChangeStat(pData);
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (pState.isReadingInfo) {
            inputVec = Vector2.zero;
            return;
        }

        inputVec = moveBuffer;
        moveBuffer = Vector2.zero;
    }

    private void FixedUpdate(){
        rigid.linearVelocity = Vector2.zero;

        if (inputVec == Vector2.zero) 
            return;

       Vector2 nxtVec= inputVec.normalized * pData.Speed.levelTables[0].spd * Time.fixedDeltaTime; //수정 필요

        rigid.MovePosition(rigid.position + nxtVec);
    }

    private void LateUpdate()
    {
        pAnimator.SetFloat("speed", inputVec.magnitude);
        //방향 조절
        if (inputVec.x != 0) {
            bool isFlipped = inputVec.x < 0;
            pSprite.flipX = isFlipped;
            playerPivot.localRotation = Quaternion.Euler(
                 playerPivot.localRotation.eulerAngles.x,
                 isFlipped ? 180f : 0f,
                 playerPivot.localRotation.eulerAngles.z);
        }
    }

    void OnKeyboardAction(KeyEvent keyEvent) {
        if (pState.isReadingInfo)
            return;

        switch (keyEvent) {
            case KeyEvent.Up:
                moveBuffer.y = 1;
                break;
            case KeyEvent.Down:
                moveBuffer.y = -1; 
                break;
            case KeyEvent.Left:
                moveBuffer.x = -1; 
                break;
            case KeyEvent.Right:
                moveBuffer.x = 1;
                break;
        }
    }

    #region 능력치 관련
    public void ChangeStat(PlayerData newData) {
        pData = newData;
        pState = new PlayerState(newData);
        pAnimator.runtimeAnimatorController = pAniControllers[pData.ID];
    }

    public void Upgrade(){
        Debug.Log("능력치 강화됨");
    }

    public bool CanUpgrade(){
        return true;
    }


    public bool GetUnlockState(){
        return true;
    }

    public string GetDescription(){
        string info = "";
        info += $"    Null++ \n";
        return info + "}";
    }
    public Sprite GetIcon(){
        return defaultStatIcon;
    }

    public void UpgradeStat(UpgradeType type){
        switch (type)
        {
            case UpgradeType.Hp:
                break;
            case UpgradeType.Speed:
                break;
        }
    }

    #endregion

    public void ReadUIInfo() {
        pState.isReadingInfo = true;
    }

    public void StopReadUIInfo() {
        pState.isReadingInfo = false;
    }
    public void InitPlayerInSurvival()
    {
        pPosition.position = Vector3.zero;
        playerPivot.rotation = Quaternion.identity;
        pSprite.flipX = false;
    }

    public void InitPlayerInVillagel()
    {
        ChangeStat(pData); //능력치 초기화
        pPosition.position = new Vector3(-7f, -0.8f, 0);
        playerPivot.rotation = Quaternion.identity;
        pSprite.flipX = false;
    }
}
