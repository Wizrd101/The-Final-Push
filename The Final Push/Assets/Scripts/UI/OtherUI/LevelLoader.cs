using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingscreen;
    public Slider slider;
    public TextMeshProUGUI progresstext;

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingscreen.SetActive(true);

        while (!operation.isDone)
        {
            float porgress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = porgress;
            progresstext.text = porgress * 100 + "%";

          
            yield return null;
        }
        Debug.Log("Finished");
    }
}
