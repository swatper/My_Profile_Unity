using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    [Header("Player Info")]
    [Tooltip("캐릭터 능력치")]
    [SerializeField] PlayerData pData;
    [Tooltip("실시간 데이터 처리용")]
    [SerializeField] PlayerState pState;
    [Header("Player Component")]
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] private SpriteRenderer pSprite;
    [SerializeField] Animator pAnimator;
    [SerializeField] AnimatorOverrideController[] pAniControllers;
    [SerializeField] Transform playerPivot;
    public Vector2 inputVec;
    public Vector2 moveBuffer;
    private void Awake()
    {
        GameManager.Input.SubscribeKeyEvent(OnKeyboardAction);
        rigid =  GetComponent<Rigidbody2D>();
        pSprite =  GetComponent<SpriteRenderer>();
        pAnimator = GetComponent<Animator>();
        ChangeStat(pData);
        DontDestroyOnLoad(gameObject);
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
        if (inputVec == Vector2.zero)
            return;
       Vector2 nxtVec= inputVec.normalized * pState.curSpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nxtVec);
    }

    private void LateUpdate()
    {
        pAnimator.SetFloat("speed", inputVec.magnitude);
        if (inputVec.x != 0) {
            bool isFlipped = inputVec.x < 0;
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
}
