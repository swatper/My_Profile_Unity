using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
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


    public void SceneReady() => isDone = true;

    void UpdateLoadingUI(float progress)
    {
        progressBar.value = progress;
        progressText.text = $"{Mathf.RoundToInt(progress * 100)}%";
    }

    public void LoadScene(string sceneName) {
        isLoading = true;
        targertScene = sceneName;
        canvas.Play("FadeIn");
    }

    public void StartLoading() {
        StartCoroutine(LoadAsync(targertScene));
    }

    void ResetLoadingState()
    {
        GameManager.Player.StopReadUIInfo();
        isDone = false;
        isLoading = false;
        progressBar.value = 0f;
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

        ResetLoadingState();
    }
}
