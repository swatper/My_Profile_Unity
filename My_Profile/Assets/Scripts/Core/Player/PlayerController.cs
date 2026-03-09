using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
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
    [SerializeField] Animator pAnimator;
    [SerializeField] AnimatorOverrideController[] pAniControllers;
    [SerializeField] Transform playerPivot;
    [SerializeField] Transform pPosition;
    public Vector2 inputVec;
    public Vector2 moveBuffer;
    [Header("Weapon Settings")]
    [SerializeField] GameObject weaponContainer;
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
        rigid.velocity = Vector2.zero;

        if (inputVec == Vector2.zero) 
            return;
 
       Vector2 nxtVec= inputVec.normalized * pState.curSpeed * Time.fixedDeltaTime;
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

    void OnKeyboardAction(Define.KeyEvent keyEvent) {
        if (pState.isReadingInfo)
            return;

        switch (keyEvent) {
            case Define.KeyEvent.Up:
                moveBuffer.y = 1;
                break;
            case Define.KeyEvent.Down:
                moveBuffer.y = -1; 
                break;
            case Define.KeyEvent.Left:
                moveBuffer.x = -1; 
                break;
            case Define.KeyEvent.Right:
                moveBuffer.x = 1;
                break;
        }
    }

    public void ChangeStat(PlayerData newData) {
        pData = newData;
        pState = new PlayerState(newData);
        pAnimator.runtimeAnimatorController = pAniControllers[pState.pID];
    }

    public void ReadUIInfo() {
        pState.isReadingInfo = true;
    }

    public void StopReadUIInfo() {
        pState.isReadingInfo = false;
    }
    public void InitPlayerInSurvival()
    {
        pPosition.position = Vector3.zero;
        pPosition.rotation = Quaternion.identity;
        pSprite.flipX = false;
        SetWeapon(0);
    }

    public void SetWeapon(int wID) {
        if (weaponContainer == null)
        {
            weaponContainer = new GameObject("Weapon Container");
            weaponContainer.transform.SetParent(playerPivot);
            weaponContainer.transform.localPosition = Vector3.zero;
            weaponContainer.transform.localRotation = Quaternion.identity;
        }
        switch (wID) {
            case 1:
                //C# 지팡이
                GameManager.Resource.Instantiate("Weapon/CsharpStaff", weaponContainer.transform);
                break;
            case 2:
                //Unity 책
                GameManager.Resource.Instantiate("Weapon/", weaponContainer.transform);
                break;
                //Flutter
            case 3:
                GameManager.Resource.Instantiate("Weapon/", weaponContainer.transform);
                break;
            default:
                //기본무기: Cpp 쇠뇌
                GameManager.Resource.Instantiate("Weapon/CppCrossbow", weaponContainer.transform);
                break;
        }
    
    }
    public void InitPlayerInVillagel()
    {
        Destroy(weaponContainer);
        pPosition.position = new Vector3(-7f, -0.8f, 0);
        playerPivot.rotation = Quaternion.identity;
        pSprite.flipX = false;
    }
}
