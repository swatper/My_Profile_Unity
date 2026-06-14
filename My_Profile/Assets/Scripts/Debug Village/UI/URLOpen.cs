using System.Collections.Generic;
using UnityEngine;

public class URLOpen : MonoBehaviour
{
    [SerializeField] List<string> urlList;

    public void OpenURL(int index) {
        Application.OpenURL(urlList[index]);
    }

    /// <summary>
    /// 동적으로 URL을 추가할 경우에 사용
    /// </summary>
    /// <param name="url"></param>
    public void AddURL(string url){
        urlList.Add(url);
    }
}
