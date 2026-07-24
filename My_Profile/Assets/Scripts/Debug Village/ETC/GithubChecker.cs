using Core.Data.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GithubChecker : MonoBehaviour
{
    [Tooltip("밭의 수 = 최근 14일 기준")]
    [SerializeField] CommitField[] commitFields;

    /// <summary>
    /// 기록 정보 현지화 및 인게임 반영
    /// </summary>
    /// <param name="comitList"></param>
    public void SetCommitInfo(GithubEventList comitList)
    {
        //각 기록 정보 확인
        Dictionary<string, string> commitDateTimeMap = new Dictionary<string, string>();
        if (comitList != null && comitList.events != null)
        {
            foreach (var githubEvent in comitList.events)
            {
                //PushEvent만 필터링
                if (githubEvent.type == "PushEvent" && !string.IsNullOrEmpty(githubEvent.created_at))
                {
                    //날짜 중복 방지
                    commitDateTimeMap[githubEvent.localDate] = githubEvent.localTime;
                }
            }
        }

        DateTime today = DateTime.Today;

        ///Commit 잔디에 반영 (0번째부터 14일전 ~ 13번째까지 당일)
        for (int i = 0; i < commitFields.Length; i++)
        {
            //이미 기록이 확인된 상태면 스킵
            if (commitFields[i].isCommited)
                continue;

            //i일 전의 날짜 문자열 생성 
            DateTime dateValue = today.AddDays(-i);
            string searchTag = dateValue.ToString("yyyy-MM-dd");    //찾으려는 최종 날짜
            string displayDate = dateValue.ToString("MM.dd");           //Commit 잔디에 표시할 날짜

            if (commitDateTimeMap.TryGetValue(searchTag, out string commitTime))
            {
                commitFields[i].InitFiled(true, displayDate, commitTime);
                // Debug.Log($"{displayDate} {commitTime} : 커밋 확인됨!");
            }
            else
            {
                commitFields[i].InitFiled(false, displayDate);
                // Debug.Log($"{displayDate} : 기록 없음.");
            }
        }
    }
}