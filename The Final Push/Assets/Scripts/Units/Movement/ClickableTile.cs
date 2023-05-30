using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableTile : MonoBehaviour
{
    public int tileX;
    public int tileY;
    public TileMap map;

    void OnMouseUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            map.GeneratePathTo(tileX, tileY);
        }
    }
}
