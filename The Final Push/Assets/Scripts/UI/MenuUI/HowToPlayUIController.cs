using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayUIController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("LevelOneScene");
    }
    
    public void BackToMenu()
    {
        SceneManager.LoadScene("main menu");
    }
}
