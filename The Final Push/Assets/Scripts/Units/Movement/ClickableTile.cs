using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

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
                        actionDist = 2;
                    }
                    else if (asc.whichAction == 2 || asc.whichAction == 4)
                    {
                        actionDist = 3;
                    }
                    else if (asc.whichAction == 3)
                    {
                        actionDist = 5;
                    }
                    else
                    {
                        Debug.LogWarning("ActionStateController to ClickableTile: whichAction transfer data error");
                    }

                    map.GeneratePathTo(tileX, tileY, actionDist);

                    Node clickTarget = map.graph[tileX, tileY];
                    Debug.Log(clickTarget.x + " " + clickTarget.y);

                    List<Unit> allUnits = new List<Unit>();

                    /*GameObject[] allUnits = null;
                    allUnits.AddRange(GameObject.FindGameObjectsWithTag("PlayerUnit"));
                    allUnits.AddRange(GameObject.FindGameObjectsWithTag("PlayerGeneral"));
                    Debug.Log(allUnits.Length);*/

                    foreach (GameObject unit in GameObject.FindGameObjectsWithTag("PlayerUnit"))
                    {
                        allUnits.Add(unit.GetComponent<Unit>());
                    }

                    foreach (GameObject unit in GameObject.FindGameObjectsWithTag("PlayerGeneral"))
                    {
                        allUnits.Add(unit.GetComponent<Unit>());
                    }

                    Debug.Log(allUnits.Count);
                }
            }
        }
    }
}
