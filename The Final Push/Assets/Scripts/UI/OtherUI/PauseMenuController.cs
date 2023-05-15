using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    Canvas pauseCanvas;

    void Awake()
    {
        pauseCanvas = GetComponent<Canvas>();
    }

    void Start()
    {
        pauseCanvas.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Pause();
            }
            else if (Time.timeScale == 0)
            {
                Resume();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseCanvas.enabled = true;
        Cursor.visible = true;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseCanvas.enabled = false;
        Cursor.visible = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("main menu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
