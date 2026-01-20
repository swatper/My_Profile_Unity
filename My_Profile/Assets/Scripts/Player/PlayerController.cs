using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    [Header("Player Info")]
    //[SerializeField] string playerName = "Tester";
    [SerializeField] float playerSpeed = 5.0f;
    [SerializeField] public bool isReadingInfo = false;
    [SerializeField] int pType = 0; //È®ÀÎ¿ë
    [Header("Player Component")]
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] private SpriteRenderer pSprite;
    [SerializeField] Animator pAnimator;
    [SerializeField] AnimatorOverrideController[] pAniControllers;
    [SerializeField] Transform lightPivot;
    public Vector2 inputVec;
    private void Awake()
    {
        //GameManager.Input.SubscribeKeyEvent(PlayerControll);
        rigid =  GetComponent<Rigidbody2D>();
        pSprite =  GetComponent<SpriteRenderer>();
        pAnimator = GetComponent<Animator>();
        ChangePlayerSkin(pType);
    }

    void Update()
    {
        if (isReadingInfo) {
            inputVec = Vector2.zero;
            return;
        }

        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate(){
       Vector2 nxtVec= inputVec.normalized * playerSpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nxtVec);
    }

    private void LateUpdate()
    {
        pAnimator.SetFloat("speed", inputVec.magnitude);
        if (inputVec.x != 0) {
            bool isFlipped = inputVec.x < 0;
            pSprite.flipX = isFlipped;
            lightPivot.localRotation = Quaternion.Euler(
                 lightPivot.localRotation.eulerAngles.x,
                 isFlipped ? 180f : 0f,
                 lightPivot.localRotation.eulerAngles.z);
        }
    }

    public void ChangePlayerSkin(int pType) {
       this.pType = pType;
        pAnimator.runtimeAnimatorController = pAniControllers[pType];
    }
}
