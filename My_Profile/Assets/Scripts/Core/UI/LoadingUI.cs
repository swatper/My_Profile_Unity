using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] GameObject loadingPanel;
    [SerializeField] Animator canvas;
    [SerializeField] string targertScene;
    [SerializeField] bool isDone;
    public bool isLoading;
    [SerializeField] Slider progressBar;
    [SerializeField] Text progressText;
    [SerializeField] string[] loadingComment;
    int lastCommentIndex;

    private void Awake()
    {
        string startMSG = loadingComment[0];
        string endMSG = loadingComment[1];

        loadingComment = new string[101];
        //문자열 최적화: StringBuilder로 가비지 없이 문장 조립 (조합할 공간 미리 할당)
        System.Text.StringBuilder sb = new System.Text.StringBuilder(64);

        for (int i = 0; i <= 100; i++)
        {
            sb.Clear();
            if (i <= 90)
                sb.Append(startMSG);
            else
                sb.Append(endMSG);

            sb.Append("(").Append(i).Append("%)");

            loadingComment[i] = sb.ToString();
        }
        progressText.text = loadingComment[0];
        loadingPanel.SetActive(false);
    }

    public void SceneReady() => isDone = true;

    void UpdateLoadingUI(float progress)
    {
        int currentIndex = Mathf.Clamp(Mathf.RoundToInt(progress * 100), 0, 100);
        if (currentIndex != lastCommentIndex)
        {
            lastCommentIndex = currentIndex;
            progressText.text = loadingComment[currentIndex];
        }
    }

    public void LoadScene(string sceneName) {
        if (isLoading)
            return;
        loadingPanel.SetActive(true);
        isLoading = true;
        targertScene = sceneName;
        canvas.Play("FadeIn");
        if (sceneName == "Dev Survival")
            GameManager.Instance.StopTimer();
        else if (sceneName == "Debug Village")
            GameManager.Instance.StartTimer();
    }

    //Animation Event에서 사용 중
    public void StartLoading() {
        StartCoroutine(LoadAsync(targertScene));
    }

    public void ResetLoadingState()
    {
        GameManager.Player.StopReadUIInfo();
        isDone = false;
        isLoading = false;
        progressBar.value = 0f;
        loadingPanel.SetActive(false);
    }

    IEnumerator LoadAsync(string sceneName)
    {
        isDone = false;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        float progress = 0.0f;

        //실제 데이터 Loading(0% ~ 90%)
        while (operation.progress < 0.9f)
        {
            progress = Mathf.Clamp01(operation.progress / 0.9f);
            UpdateLoadingUI(progress);
            yield return null;
        }

        //구 씬을 삭제하고 새로운 씬을 활성화
        while (progressBar.value < 0.99f)
        {
            progressBar.value = Mathf.MoveTowards(progressBar.value, 1f, Time.unscaledDeltaTime);
            UpdateLoadingUI(progressBar.value);
            yield return null;
        }

        operation.allowSceneActivation = true;

        //씬 로딩 완료 신호 기다리기
        while (!isDone) {
            yield return null;
        }

        UpdateLoadingUI(1f);
        yield return new WaitForSecondsRealtime(0.5f);

        canvas.Play("FadeOut");
    }
}
