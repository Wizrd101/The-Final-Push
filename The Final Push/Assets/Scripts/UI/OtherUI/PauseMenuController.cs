using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
    }
}
