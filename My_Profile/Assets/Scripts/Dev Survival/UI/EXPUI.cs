using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EXPUI : MonoBehaviour
{
    [SerializeField] Slider expBar;
    [SerializeField] Text levelText;
    private const string preText = "Lv.  ";
    [SerializeField] Text timeText;
    private float remainingTime = 300f;
    private int currentStage = 1;               //추후 제거 예정
    private float nextStageTime = 24f;
    private float timerAfterStageUp = 0f;
    private float sValue = 4f;

    private void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            timerAfterStageUp += Time.deltaTime;

            //단계 상승 로직 (20 + 단계 x 4)
            if (currentStage < 9 && timerAfterStageUp >= nextStageTime){
                currentStage++;
                SurvivalSceneDirector.Instance.PaseUp();
                timerAfterStageUp = 0f;
                nextStageTime = 20f + (currentStage * sValue);
                Debug.Log($"단계 상승! 현재: 다음 상승까지: {nextStageTime}초");
            }
            DisplayTime(remainingTime);
        }
    }

    public void SetMaxEXP(float MaxExp) {
        expBar.maxValue = MaxExp;
    }

    public void UpdateUI(float currentExp, int level){
        expBar.value = currentExp;
        levelText.text = preText + level;
    }

    void DisplayTime(float timeToDisplay) {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
