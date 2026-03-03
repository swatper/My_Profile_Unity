using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EXPUI : MonoBehaviour
{
    [SerializeField] Slider expBar;
    [SerializeField] Text levelText;
    string preText = "Lv. ";

    void Awake() {
        expBar.value = 0;
        levelText.text = preText + '1';
    }
    public void SetMaxEXP() { }
    public void GainEXP() { }
}
