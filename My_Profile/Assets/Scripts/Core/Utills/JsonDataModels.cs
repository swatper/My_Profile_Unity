using UnityEngine;

//더 추가 예정
namespace Core.Data.Json
{
    /// <summary>
    /// GIST에 있는 유니티 에셋 쿠폰 정보
    /// </summary>
    [System.Serializable]
    public class CouponData
    {
        public string publisher_name;
        public string asset_name;
        public string coupon_code;
        public string asset_url;
        public string last_updated;
    }
}

