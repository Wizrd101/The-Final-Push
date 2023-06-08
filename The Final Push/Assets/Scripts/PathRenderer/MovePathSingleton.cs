using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePathSingleton : MonoBehaviour
{
    MovePathRenderer mpr;
    TileMap map;

    void Update()
    {
        if (map.currentPath == null)
        {
            return;
        }
        else
        {
            foreach (Node n in map.currentPath)
            {
                
            }
        }
    }
}
