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

    StateController sm;

    void OnMouseUp()
    {
        sm = map.selectedUnit.GetComponent<StateController>();
        if (!EventSystem.current.IsPointerOverGameObject() && sm.state == UnitState.MOVING)
        {
            map.GeneratePathTo(tileX, tileY);
        }
    }
}
