using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NeverEnding : MonoBehaviour
{
    void Awake()
    {
        Scene curScene = SceneManager.GetActiveScene();
        if (curScene.buildIndex == 1)
        {
            DontDestroyOnLoad(transform.gameObject);
        }
    }
}
