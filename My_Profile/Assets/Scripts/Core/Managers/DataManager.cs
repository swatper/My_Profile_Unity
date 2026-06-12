using Core.Data.Json;
using UnityEngine;

/// <summary>
/// 주로 통신 데이터(Git commit, 무료 에셋 쿠폰) 캐싱
/// </summary>
public class DataManager
{
    public string GithubJsonText { get; set; }
    public CouponData AssetCoupon { get;  set; }
}
