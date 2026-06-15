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

        //각 기록 정보 확인 (Hash를 사용해 빠름)
        HashSet<string> commitDates = new HashSet<string>();
        if (tmp != null && tmp.events != null)
        {
            foreach (var githubEvent in tmp.events)
            {
                //PushEvent만 필터링
                if (githubEvent.type == "PushEvent" && !string.IsNullOrEmpty(githubEvent.created_at)){
                    //"2026-06-14T04:28:31Z" -> 앞의 "2026-06-14"만 잘라내기
                    string dateOnly = githubEvent.created_at.Substring(0, 10);
                    commitDates.Add(dateOnly);
                }
            }
        }

        ///Commit 잔디에 반영 (0번째부터 14일전 ~ 13번째까지 당일)
        for (int i = 0; i < commitFields.Length; i ++){
            //이미 기록이 확인된 상태면 스킵
            if (commitFields[i].isCommited)
                continue;

            //날짜 선택 (UTC 기준)
            DateTime dateValue = DateTime.UtcNow.AddDays(-i);
            //i일 전의 날짜 문자열 생성 
            string searchTag = dateValue.ToString("yyyy-MM-dd");    //찾으려는 최종 날짜
            string displayDate = dateValue.ToString("MM.dd");           //Commit 잔디에 표시할 날짜

            //단순 commit 유무만 확인
            bool isCommited = commitDates.Contains(searchTag);
            if (isCommited){
                commitFields[i].InitFiled(true, displayDate);
                //Debug.Log($"{displayDate} : 커밋 확인됨!");
            }
            else{
                commitFields[i].InitFiled(false, displayDate);
                //Debug.Log($"{displayDate} : 기록 없음.");
            }
        }
    }
}