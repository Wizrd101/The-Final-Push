using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{
    void Start()
    {
        
    }

    public void  StartGame()
    {
        SceneManager.LoadScene("intro");
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene("How to Play");
    }

    public void Credits()
    {
        SceneManager.LoadScene("credits");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
