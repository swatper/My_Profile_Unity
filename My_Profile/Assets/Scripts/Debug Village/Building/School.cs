using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class School : BuildingBase
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            uiScript.Show();
    }
}
