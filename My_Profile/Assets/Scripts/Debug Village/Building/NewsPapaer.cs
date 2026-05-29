using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using  Core.Data.Json;
using UnityEngine.UI;
using System.Runtime.InteropServices;   //웹GL 플로그인

public class NewsPapaer : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void CopyTextToClipboardWebGL(string text);

    [Header("Gist Settings")]
    [SerializeField] private string gistRawUrl;
    [SerializeField] URLOpen urlOpenner;
    [Header("UI 요소")]
    [SerializeField] Text assetName;
    [SerializeField] Text assetPublisherName;
    [SerializeField] Text couponCode;
    [SerializeField] Text updateDate;

    [Header("쿠폰 정보")]
    public CouponData LoadedCoupon { get; private set; }

    void Awake()
    {
        StartCoroutine(FetchGistData());
    }

    public void CopyCoupon()
    {
        if (LoadedCoupon == null || string.IsNullOrEmpty(LoadedCoupon.coupon_code))
            return;

        string textToCopy = LoadedCoupon.coupon_code;

#if !UNITY_EDITOR && UNITY_WEBGL
        //🌐 WebGL 빌드 환경일 때 실행
        try
        {
            CopyTextToClipboardWebGL(textToCopy);
        }
        catch (System.EntryPointNotFoundException e)
        {
            Debug.LogError("WebGL 클립보드 플러그인을 찾을 수 없습니다: " + e.Message);
        }
#else
        //💻 유니티 에디터나 PC 빌드 환경일 때 실행
        GUIUtility.systemCopyBuffer = textToCopy;
#endif

    }

    IEnumerator FetchGistData()
    {
        //Gist로부터 쿠폰 정보 가져오기
        using (UnityWebRequest webRequest = UnityWebRequest.Get(gistRawUrl))
        {
            yield return webRequest.SendWebRequest();

            //에러 체크
            if (webRequest.result == UnityWebRequest.Result.Success){
                string jsonText = webRequest.downloadHandler.text;
                Debug.Log($"Gist 원본 JSON 수신 성공:\n{jsonText}");

                //JsonUtility로 C# 객체에 쏙 파싱하기
                LoadedCoupon = JsonUtility.FromJson<CouponData>(jsonText);
                //UI에 할당
                assetName.text = "Asset Name: " + LoadedCoupon.asset_name;
                assetPublisherName.text = "Publisher: " + LoadedCoupon.publisher_name;
                couponCode.text = LoadedCoupon.coupon_code;
                updateDate.text = "Last Updated: " + LoadedCoupon.last_updated;
                urlOpenner.AddURL(LoadedCoupon.asset_url);
            }
            else{
                Debug.LogError($"쿠폰 정보 가져오기 실패: {webRequest.error}");
            }
        }
    }
}
