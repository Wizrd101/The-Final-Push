using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class introUI : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("LevelOneScene");
    }

    public void howtoplay()
    {
        SceneManager.LoadScene("How to play");
    }
}

    

