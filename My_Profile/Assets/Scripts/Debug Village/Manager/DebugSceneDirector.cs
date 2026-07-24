using Core.Data.Json;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// 전체 씬 관리자이자 MVC 패턴의 Controller
/// </summary>
public class DebugSceneDirector : BaseSceneDirector
{
    public static DebugSceneDirector Instance { get; private set; }
    bool IsSceneProtected = false;
    [SerializeField] GithubChecker commitField;
    [SerializeField] GameObject checkUI;
    [SerializeField] InputField code;
    [SerializeField] Text message;
    public VillageSoundController soundController;
    [Header("MVC 패턴: 에셋 쿠폰 정보")]
    [SerializeField] private string gistRawUrl;
    [Tooltip("Model")]
    public CouponData loadedCoupon;
    [Tooltip("View")]
    public NewsPapaer newsPaper;
    [Header("MVC 패턴: Commit 정보")]
    [SerializeField] private string githubAPI = "https://api.github.com/users/swatper/events/public?per_page=100";
    [Tooltip("Model")]
    public GithubEventList commitList;
    [Tooltip("View")]
    public GithubChecker commitfield;


    private void Awake(){
        Instance = this;
        StartCoroutine(FetchGistData());
        StartCoroutine(GetGithubEvents());
    }

    protected override void InitScene()
    {
        base.InitScene();
        GameManager.Player.InitPlayerInVillagel();
        GameManager.Player.StopReadUIInfo();
        SceneReady();
    }

    public override void MuteSound(){
        soundController.MuteAllSound();
    }

    public override void UnMuteSound(){
        soundController.UnMuteAllSound();
    }

//#if UNITY_EDITOR
    /// <summary>
    /// 개발 완료 후 되돌릴 예정
    /// </summary>
    public void OpenCheckUI() {
        if (!IsSceneProtected) {
            GoToScene();
        }
        else{
            GameManager.Player.ReadUIInfo();
            checkUI.SetActive(true);
            code.text = "";
            message.text = "Insert Code:";
            code.ActivateInputField();
        }
    }

    public void CheckCode() {
        if (code.text == "I'm not a robot") {
            checkUI.SetActive(false);
            GoToScene();
        }
        else{
            message.text = "Wrong Code";
        }
    }

    public void CloseCheckUI() {
        GameManager.Player.StopReadUIInfo();
        checkUI.SetActive(false);
    }
    //#endif

    #region 정보 가져오기
    /// <summary>
    /// 쿠폰 정보 가져오기
    /// </summary>
    /// <returns></returns>
    IEnumerator FetchGistData()
    {
        loadedCoupon = GameManager.Data.AssetCoupon;
        if (loadedCoupon == null)
        {
            //Gist로부터 쿠폰 정보 가져오기
            using (UnityWebRequest webRequest = UnityWebRequest.Get(gistRawUrl))
            {
                yield return webRequest.SendWebRequest();

                //에러 체크
                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    string jsonText = webRequest.downloadHandler.text;
                    Debug.Log($"받은 쿠폰 Json 데이터: \n{jsonText}");

                    //JsonUtility로 C# 객체에 쏙 파싱하기
                    loadedCoupon = JsonUtility.FromJson<CouponData>(jsonText);
                    GameManager.Data.AssetCoupon = loadedCoupon;
                }
                else{
                    Debug.LogError($"쿠폰 정보 가져오기 실패: {webRequest.error}");
                }
            }
        }

        //View에 할당
        newsPaper.SetCouponInfo(loadedCoupon);
    }
    IEnumerator GetGithubEvents()
    {
        commitList = GameManager.Data.GitEvent;
        if (commitList == null)
        {
            //Debug.Log("캐싱된 Commit 기록 없음");
            string githubLog = "";
            using (UnityWebRequest webRequest = UnityWebRequest.Get(githubAPI))
            {
                yield return webRequest.SendWebRequest();


                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    githubLog = webRequest.downloadHandler.text;
                    Debug.Log("받은 Github Json 데이터: \n" + githubLog);
                }
                else
                {
                    Debug.LogError("GitHub API 요청 실패: " + webRequest.error);
                    yield break;
                }
            }

            string wrappedJson = "{\"events\":" + githubLog + "}";
            //전체 기록 정보
            commitList = JsonUtility.FromJson<GithubEventList>(wrappedJson);
            GameManager.Data.GitEvent = commitList;
        }
        commitfield.SetCommitInfo(commitList);
    }
    #endregion
}
