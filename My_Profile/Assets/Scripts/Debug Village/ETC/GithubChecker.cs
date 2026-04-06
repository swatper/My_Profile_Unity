using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public class GithubChecker : MonoBehaviour
{
    private string username = "swatper";
    [SerializeField] CommitField[] commitFields;

    public void CheckCommit(){
        StartCoroutine(GetGithubEvents());
    }

    IEnumerator GetGithubEvents()
    {
        string url = $"https://api.github.com/users/{username}/events/public?per_page=100";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url)){
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string json = webRequest.downloadHandler.text;
                Debug.Log("받은 Json 데이터: \n" + json);

                for (int i = 0; i < 14; i++){
                    //i일 전의 날짜 문자열 생성 (UTC 기준)
                    string targetDate = DateTime.UtcNow.AddDays(-i).ToString("yyyy-MM-dd");
                    //단순 commit 유무만 확인
                    bool isCommited = json.Contains(targetDate) && json.Contains("PushEvent");

                    if (isCommited){
                        commitFields[i].InitFiled(true, targetDate);
                        Debug.Log($"{targetDate} : 커밋 확인됨!");
                    }
                    else{
                        commitFields[i].InitFiled(false, targetDate);
                        Debug.Log($"{targetDate} : 기록 없음.");
                    }
                }
            }
            else
                Debug.LogError("GitHub API 요청 실패: " + webRequest.error);
        }
    }
}