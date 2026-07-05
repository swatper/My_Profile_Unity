using Core.Data.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GithubChecker : MonoBehaviour
{
    private string username = "swatper";
    [Tooltip("밭의 수 = 최근 14일 기준")]
    [SerializeField] CommitField[] commitFields;
    

    public void CheckCommit(){
        StartCoroutine(GetGithubEvents());
    }

    IEnumerator GetGithubEvents()
    {
        GithubEventList tmp = GameManager.Data.GitEvent;
        if (tmp == null){
            //Debug.Log("캐싱된 Commit 기록 없음");
            string githubLog = "";
            string url = $"https://api.github.com/users/{username}/events/public?per_page=100";
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                yield return webRequest.SendWebRequest();
                if (webRequest.result == UnityWebRequest.Result.Success){
                    githubLog = webRequest.downloadHandler.text;
                    Debug.Log("받은 Github Json 데이터: \n" + githubLog);
                }
                else{
                    Debug.LogError("GitHub API 요청 실패: " + webRequest.error);
                    yield break;
                }
            }

            string wrappedJson = "{\"events\":" + githubLog + "}";
            //전체 기록 정보
            tmp  = JsonUtility.FromJson<GithubEventList>(wrappedJson);
            GameManager.Data.GitEvent = tmp;
        }

        //각 기록 정보 확인
        Dictionary<string, string> commitDateTimeMap = new Dictionary<string, string>();
        if (tmp != null && tmp.events != null)
        {
            foreach (var githubEvent in tmp.events)
            {
                //PushEvent만 필터링
                if (githubEvent.type == "PushEvent" && !string.IsNullOrEmpty(githubEvent.created_at)){
                    //날짜 중복 방지
                    commitDateTimeMap[githubEvent.localDate] = githubEvent.localTime;
                }
            }
        }


        DateTime today = DateTime.Today;

        ///Commit 잔디에 반영 (0번째부터 14일전 ~ 13번째까지 당일)
        for (int i = 0; i < commitFields.Length; i ++){
            //이미 기록이 확인된 상태면 스킵
            if (commitFields[i].isCommited)
                continue;

            //i일 전의 날짜 문자열 생성 
            DateTime dateValue = today.AddDays(-i);
            string searchTag = dateValue.ToString("yyyy-MM-dd");    //찾으려는 최종 날짜
            string displayDate = dateValue.ToString("MM.dd");           //Commit 잔디에 표시할 날짜

            if (commitDateTimeMap.TryGetValue(searchTag, out string commitTime)){
                commitFields[i].InitFiled(true, displayDate, commitTime);
                // Debug.Log($"{displayDate} {commitTime} : 커밋 확인됨!");
            }
            else{
                commitFields[i].InitFiled(false, displayDate);
                // Debug.Log($"{displayDate} : 기록 없음.");
            }
        }
    }
}