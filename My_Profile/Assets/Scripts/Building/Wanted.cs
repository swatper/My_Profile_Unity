using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wanted : BuildingBase
{
    [SerializeField] WANTED_UI uiScript;
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        uiScript.Show();
    }
}
