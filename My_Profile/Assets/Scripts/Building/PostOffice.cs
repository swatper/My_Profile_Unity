using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostOffice : BuildingBase
{
    string email1 = "mailto:sh010510@naver.com";
    string email2 = "mailto:kg010511@gmail.com";
    string discord = "https://discord.com/users/381621213597794314";


    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            uiScript.Show();
    }

    public void Contact(string type) {
        string targetURL = "";
        switch (type) {
            case "Email1":
                targetURL = email1;
                break;
            case "Email2":
                targetURL = email2;
                break;
            case "Discord":
                targetURL = discord;
                break;
        }
        Application.OpenURL(targetURL);
    }
}
