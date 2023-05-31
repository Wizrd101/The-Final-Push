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

    UnitInfo unit;
    GeneralInfo general;

    StateController sc;

    void OnMouseUp()
    {
        sc = map.selectedUnit.GetComponent<StateController>();
        if (!EventSystem.current.IsPointerOverGameObject() && sc.state == UnitState.MOVING)
        {
            if (map.selectedUnit.tag == "PlayerUnit")
            {
                unit = map.selectedUnit.GetComponent<UnitInfo>();
                map.GeneratePathTo(tileX, tileY, unit.moveDist);
            }
            else
            {
                general = map.selectedUnit.GetComponent<GeneralInfo>();
                map.GeneratePathTo(tileX, tileY, general.moveDist);
            }
        }
    }
}
