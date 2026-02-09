using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Temple : BuildingBase
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            uiScript.Show();
    }
}
