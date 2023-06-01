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

    ActionStateController asc;

    int actionDist = 0;

    void OnMouseUp()
    {
        if (map.selectedUnit != null)
        {
            sc = map.selectedUnit.GetComponent<StateController>();
            asc = map.selectedUnit.GetComponent<ActionStateController>();
        }

        if (map.selectedUnit != null && !EventSystem.current.IsPointerOverGameObject())
        {
            if (sc.state == UnitState.MOVING)
            {
                if (map.selectedUnit.tag == "PlayerUnit")
                {
                    unit = map.selectedUnit.GetComponent<UnitInfo>();
                    map.GeneratePathTo(tileX, tileY, unit.moveDist);
                }
                else if (map.selectedUnit.tag == "PlayerGeneral")
                {
                    general = map.selectedUnit.GetComponent<GeneralInfo>();
                    map.GeneratePathTo(tileX, tileY, general.moveDist);
                }
            }
            else if (sc.state == UnitState.ACTION)
            {
                if (asc.whichAction != 0)
                {
                    Debug.Log("Action being done");

                    if (asc.whichAction == 1)
                    {
                        actionDist = 1;
                    }
                    else if (asc.whichAction == 2 || asc.whichAction == 4)
                    {
                        actionDist = 2;
                    }
                    else if (asc.whichAction == 3)
                    {
                        actionDist = 4;
                    }
                    else
                    {
                        Debug.LogWarning("ActionStateController to ClickableTile: whichAction transfer data error");
                    }

                    map.GeneratePathTo(tileX, tileY, actionDist);
                }
            }
        }
    }
}
