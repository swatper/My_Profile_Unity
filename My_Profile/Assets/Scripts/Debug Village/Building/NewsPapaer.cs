using UnityEngine;
using  Core.Data.Json;
using UnityEngine.UI;

/// <summary>
/// MVC 패턴의 View (쿠폰 정보 UI에 할당)
/// </summary>
public class NewsPapaer : MonoBehaviour
{
    [Header("Gist Settings")]
    [SerializeField] private string gistRawUrl;
    [SerializeField] URLOpen urlOpenner;
    [Header("UI 요소")]
    [SerializeField] Text assetName;
    [SerializeField] Text assetPublisherName;
    [SerializeField] Text couponCode;
    [SerializeField] Text updateDate;

    [Header("쿠폰 정보")]
    CouponData LoadedCoupon { get; set; }

    public void CopyCoupon()
    {
        if (LoadedCoupon == null || string.IsNullOrEmpty(LoadedCoupon.coupon_code)) 
            return;

        GameManager.Native.CopyToClipboard(LoadedCoupon.coupon_code);
    }

    /// <summary>
    /// 쿠폰 정보 UI에 할당
    /// </summary>
    /// <param name="couponInfo"></param>
    public void SetCouponInfo(CouponData couponData){
        LoadedCoupon = couponData;
        //UI에 할당
        assetName.text = "Asset Name: " + couponData.asset_name;
        assetPublisherName.text = "Publisher: " + couponData.publisher_name;
        couponCode.text = couponData.coupon_code;
        updateDate.text = "Last Updated: " + couponData.last_updated;
        urlOpenner.AddURL(LoadedCoupon.asset_url);
    }
}
