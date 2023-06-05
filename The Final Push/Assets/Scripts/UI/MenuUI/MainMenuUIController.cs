using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{
    void Start()
    {
        
    }

    void StartGame()
    {
        SceneManager.LoadScene("intro");
    }

    void HowToPlay()
    {
        SceneManager.LoadScene("How to Play");
    }

    void Credits()
    {
        SceneManager.LoadScene("credits");
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
