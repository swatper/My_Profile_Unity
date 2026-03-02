using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wanted : BuildingBase
{
    //public Text ageText;
    private const int birthYear = 2001;
    private const int birthMonth = 5;
    private const int birthDay = 10;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ETC")
            return;
        uiScript.Show();
    }

    private void Start()
    {
        int curYear = GameManager.Clock.Year;
        int curMonth = GameManager.Clock.Month;
        int curDay = GameManager.Clock.Day;
        int koreanAge = curYear - birthYear + 1;
        //만 나이 계산
        int fullAge = curYear - birthYear;

        if (curMonth < birthMonth || (curMonth == birthMonth && curDay < birthDay))
            fullAge--;
    
        //ageText.text = $"Age: {koreanAge}세 (만 {fullAge}세)";
    }
}
