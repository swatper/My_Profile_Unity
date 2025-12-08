using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wanted : BuildingBase
{
    public Text ageText;
    private const int birthYear = 2001;
    private const int birthMonth = 5;
    private const int birthDay = 10;

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

    private void Awake()
    {
        int curYear = int.Parse(GameManager.Clock.localYear);
        int curMonth = int.Parse(GameManager.Clock.localMonth);
        int curDay = int.Parse(GameManager.Clock.localDay);
        int koreanAge = curYear - birthYear + 1;
        //만 나이 계산
        int fullAge = curYear - birthYear;

        if (curMonth < birthMonth || (curMonth == birthMonth && curDay < birthDay))
            fullAge--;
    
        ageText.text = $"Age: {koreanAge}세 (만 {fullAge}세)";
    }
}
