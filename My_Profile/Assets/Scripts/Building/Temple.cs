using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Temple : BuildingBase
{
    string prj1 = "https://www.notion.so/InMusic-1d2d6c7cb36580a2beb9e57919abc5ae?source=copy_link";
    string prj2 = "https://www.notion.so/My_Diary-1d2d6c7cb36580b28dcaf576096f6fd2?source=copy_link";
    string prj3 = "https://www.notion.so/1d2d6c7cb36580be8d37c0ad1522fe68?source=copy_link";
    string allPrjURL = "https://www.notion.so/1d2d6c7cb36580e8a6b2e49791043998?v=1d2d6c7cb365802eb2c3000c264ba94d&source=copy_link";
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            uiScript.Show();
    }

    public void MoreProjects(string type){
        string targetURL = "";
        switch (type)
        {
            case "Project1":
                targetURL = prj1;
                break;
            case "Project2":
                targetURL = prj2;
                break;
            case "Project3":
                targetURL = prj3;
                break;
            case "ProjectAll":
                targetURL = allPrjURL;
                break;
        }
        Application.OpenURL(targetURL);
    }
}
