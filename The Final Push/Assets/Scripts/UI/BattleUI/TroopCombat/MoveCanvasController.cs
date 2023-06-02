using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCanvasController : MonoBehaviour
{
    Canvas moveCanvas;

    TileMap map;

    void Start()
    {
        moveCanvas = GetComponent<Canvas>();
        map = GameObject.Find("Map").GetComponent<TileMap>();
    }

    void Update()
    {
        if (map.selectedUnit == null)
        {
            moveCanvas.enabled = false;
        }
        else
        {
            moveCanvas.enabled = true;
        }
    }
}
