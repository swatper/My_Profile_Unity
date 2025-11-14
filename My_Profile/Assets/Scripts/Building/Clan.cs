using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clan : BuildingBase
{
    string InfoURL = "https://www.notion.so/1d2d6c7cb36580358529f18802349d17?v=1d2d6c7cb365807fadfb000c09967e49&source=copy_link";

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            uiScript.Show();
    }


    public void MoreInfomation()
    {
        Application.OpenURL(InfoURL);
    }
}
