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
    #region Github API
    /// <summary>
    /// GitHub Event API 최상위 배열을 파싱하기 위한 래퍼 클래스
    /// </summary>
    [System.Serializable]
    public class GithubEventList
    {
        public GithubEvent[] events;
    }

    [System.Serializable]
    public class GithubEvent
    {
        public string id;
        public string type;             //"PushEvent", "CreateEvent" 등
        public RepoInfo repo;
        public string created_at;    //UTC 시간 문자열 (예: "2026-06-14T04:28:31Z")
    }

    [System.Serializable]
    public class RepoInfo
    {
        public long id;
        public string name;         //"username/repo-name"
        public string url;
    }
    #endregion
}

