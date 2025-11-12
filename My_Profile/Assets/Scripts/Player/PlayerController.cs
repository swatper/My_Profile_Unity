using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Info")]
    [SerializeField] string playerName = "Tester";
    [SerializeField] float playerSpeed = 5.0f;
    [Header("Player Data")]
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] private SpriteRenderer pSprite;
    [SerializeField] Animator pAnimator;
    public Vector2 inputVec;
    private void Awake()
    {
        //GameManager.Input.SubscribeKeyEvent(PlayerControll);
        rigidbody =  GetComponent<Rigidbody2D>();
        pSprite =  GetComponent<SpriteRenderer>();
        pAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate(){
       Vector2 nxtVec= inputVec.normalized * playerSpeed * Time.fixedDeltaTime;
        rigidbody.MovePosition(rigidbody.position + nxtVec);
    }

    private void LateUpdate()
    {
        pAnimator.SetFloat("speed", inputVec.magnitude);
        if (inputVec.x != 0) {
            pSprite.flipX = inputVec.x < 0;
        }
    }

    /// <summary>
    /// 키보드 조작
    /// </summary>
    /// <param name="key"></param>
    void PlayerControll(Define.KeyEvent key) {
        //switch (key) {
        //    case Define.KeyEvent.Left:
        //        moveDirection = new Vector2 (-1, moveDirection.y);
        //        break;
        //    case Define.KeyEvent.Right:
        //        moveDirection = new Vector2(1, moveDirection.y);
        //        break;
        //    case Define.KeyEvent.Up:
        //        moveDirection = new Vector2(moveDirection.x, 1);
        //        break;
        //    case Define.KeyEvent.Down:
        //        moveDirection = new Vector2(moveDirection.x, -1);
        //        break;
        //}
    }

    /// <summary>
    /// 마우스/터치 조작
    /// </summary>
    /// <param name="target"></param>
    public void GoPlayerToLocation(Vector2 target) {
        rigidbody.MovePosition(target);
    }
}
