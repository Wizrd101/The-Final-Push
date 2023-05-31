using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableTile : MonoBehaviour
{
    public int tileX;
    public int tileY;
    public TileMap map;

    StateController sc;

    void OnMouseUp()
    {
        sc = map.selectedUnit.GetComponent<StateController>();
        if (!EventSystem.current.IsPointerOverGameObject() && sc.state == UnitState.MOVING)
        {
            map.GeneratePathTo(tileX, tileY);
        }
    }
}
