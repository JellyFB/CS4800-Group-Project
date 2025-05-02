using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    public Slider progressBar;
    public TextMeshProUGUI progressText;

    void Start()
    {
        StartCoroutine(LoadSceneAsync("MainMenu"));
    }

    IEnumerator LoadSceneAsync(string sceneName)
{
    AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
    operation.allowSceneActivation = false; // Don't switch immediately

    float elapsedTime = 0f;
    float totalTime = 10f; // Simulated 10 seconds

    while (elapsedTime < totalTime)
    {
        elapsedTime += Time.deltaTime;
        float progress = Mathf.Clamp01(elapsedTime / totalTime);
        progressBar.value = progress;
        progressText.text = (progress * 100f).ToString("F0") + "%";
        yield return null;
    }

    // Once fake loading is done, activate the scene
    operation.allowSceneActivation = true;
}

}
