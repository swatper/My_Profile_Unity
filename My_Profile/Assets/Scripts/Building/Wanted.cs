using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wanted : BuildingBase
{
    string notionURL = "https://www.notion.so/d553e45114e04fd69fde4ed56d8afe6b?source=copy_link";
    string gitURL = "https://github.com/swatper";

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ETC")
            return;
        uiScript.Show();
    }
    public void MoreInfomation(string type)
    {
        string targetURL = "";
        switch (type) {
            case "Notion":
                targetURL = notionURL;
                break;
            case "GitHub":
                    targetURL = gitURL;
                    break;
        }
        Application.OpenURL(targetURL);
    }
    //TODO: 나이 자동으로 계산하는 기능 추가하기
}
