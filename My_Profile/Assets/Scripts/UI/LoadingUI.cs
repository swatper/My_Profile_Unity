using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] GameObject loadingPanel;
    [SerializeField] Slider progressBar;
    [SerializeField] Text progressText;
    [SerializeField] bool isDone;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void SceneReady() => isDone = true;

    void UpdateLoadingUI(float progress)
    {
        progressBar.value = progress;
        progressText.text = $"{Mathf.RoundToInt(progress * 100)}%";
    }

    public void LoadScene(string sceneName) {
        loadingPanel.SetActive(true);
        StartCoroutine(LoadAsync(sceneName));
    }

    IEnumerator LoadAsync(string sceneName)
    {
        isDone = false;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        float progress = 0.0f;

        //로딩 처리
        while (operation.progress < 0.9f)
        {
            progress = Mathf.Clamp01(operation.progress / 0.9f);
            UpdateLoadingUI(progress);
            yield return null;
        }

        while (progressBar.value < 0.99f)
        {
            progressBar.value = Mathf.MoveTowards(progressBar.value, 1f, Time.unscaledDeltaTime);
            UpdateLoadingUI(progressBar.value);
            yield return null;
        }

        //씬 로딩 완료 신호 기다리기
        while (!isDone) {
            yield return null;
        }

        UpdateLoadingUI(1f);
        yield return new WaitForSecondsRealtime(0.1f);
        operation.allowSceneActivation = true;
        isDone = false;
        loadingPanel.SetActive(false);
    }
}
