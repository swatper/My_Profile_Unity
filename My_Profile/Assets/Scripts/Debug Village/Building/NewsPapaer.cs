using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using  Core.Data.Json;
using UnityEngine.UI;

public class NewsPapaer : MonoBehaviour
{
    [Header("Gist Settings")]
    [SerializeField] private string gistRawUrl;
    [Header("쿠폰 정보")]
    [SerializeField] URLOpen urlOpenner;
    [Header("UI 요소")]
    [SerializeField] Text assetName;
    [SerializeField] Text assetPublisherName;
    [SerializeField] Text couponCode;
    [SerializeField] Text updateDate;
    
    public CouponData LoadedCoupon { get; private set; }

    void Awake()
    {
        StartCoroutine(FetchGistData());
    }

    public void CopyCoupon()
    {
        if (LoadedCoupon == null)
            return;

        GUIUtility.systemCopyBuffer = LoadedCoupon.coupon_code;
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
