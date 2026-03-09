using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EXPUI : MonoBehaviour
{
    [SerializeField] Slider expBar;
    [SerializeField] Text levelText;
    private const string preText = "Lv.  ";

   public void SetMaxEXP(float MaxExp) {
        expBar.maxValue = MaxExp;
    }

    public void UpdateUI(float currentExp, int level)
    {
        expBar.value = currentExp;
        levelText.text = preText + level;
    }
}
