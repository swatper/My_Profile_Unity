using System;
using UnityEngine;

//더 추가 예정
namespace Core.Data.Json
{
    /// <summary>
    /// GIST에 있는 유니티 에셋 쿠폰 정보 (퍼블리셔, 에셋 이름, 쿠폰 코드, 에셋 URL,  정보 업데이트 날짜)
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
    public class GithubEvent : ISerializationCallbackReceiver
    {
        public string id;
        public string type;             //"PushEvent", "CreateEvent" 등
        public RepoInfo repo;
        public string created_at;    //UTC 시간 문자열 (예: "2026-06-14T04:28:31Z")

        #region 현시 시간 한국(KST)
        [NonSerialized] public string localDate;    // "yyyy-MM-dd"
        [NonSerialized] public string localTime;    //"HH:mm:ss"
        #endregion

        /// <summary>
        /// 역직렬화: C# 객체 -> Json
        /// </summary>
        public void OnBeforeSerialize() { }

        /// <summary>
        /// 직렬화: Json -> C# 객체
        /// </summary>
        public void OnAfterDeserialize()
        {
            if (DateTime.TryParse(created_at, out DateTime utcTime))
            {
                // 한국(로컬) 시간으로 변경
                DateTime kstTime = utcTime.ToLocalTime();
                localDate = kstTime.ToString("yyyy-MM-dd");
                localTime = kstTime.ToString("HH:mm:ss");
            }
            else
            {
                localDate = "변환 실패";
                localTime = "변환 실패";
            }
        }
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

