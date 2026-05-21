using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using  Core.Data.Json;

public class NewsPapaer : MonoBehaviour
{
    [Header("Gist Settings")]
    [SerializeField] private string gistRawUrl;
    [Header("쿠폰 정보")]
    [SerializeField] URLOpen urlOpenner;
    public CouponData LoadedCoupon { get; private set; }

    void Awake()
    {
        StartCoroutine(FetchGistData());
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
                urlOpenner.AddURL(LoadedCoupon.asset_url);
            }
            else{
                Debug.LogError($"쿠폰 정보 가져오기 실패: {webRequest.error}");
            }
        }
    }
}
