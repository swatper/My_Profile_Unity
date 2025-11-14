using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class School : BuildingBase
{
    string InfoURL = "https://www.notion.so/1d2d6c7cb36580429225c8520069ecce?v=1d2d6c7cb365802d9ea4000c9e12400c&source=copy_link";
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            uiScript.Show();
    }

    public void MoreInfomation() {
        Application.OpenURL(InfoURL);
    }
}
