using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    //public GameObject loseScreen;
    //public GameObject winScreen;
    public Slider slider;
    public Text progessText;

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    public void LoadLoseGame(int sceneIndex)
    {
        StartCoroutine(LoadLoseScreenAsync(sceneIndex));
    }

    public void LoadWinGame(int sceneIndex)
    {
        StartCoroutine(LoadWinScreenAsync(sceneIndex));
    }

    IEnumerator LoadLoseScreenAsync(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex);

        //loseScreen.SetActive(true);

        yield return null;
    }

    IEnumerator LoadWinScreenAsync(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex);

        //winScreen.SetActive(true);

        yield return null;
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            progessText.text = progress * 100f + "%";

            yield return null;
        }
    }


}
