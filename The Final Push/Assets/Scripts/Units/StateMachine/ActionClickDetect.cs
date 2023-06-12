using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionClickDetect : MonoBehaviour
{
    int tempX;
    int tempY;
    TileMap map;

    Unit thisUnit;
    UnitInfo thisUnitInfo;
    GeneralInfo thisGeneralInfo;
    BuildingInfo thisBuildingInfo;
    
    Unit unit;
    UnitInfo unitInfo;
    GeneralInfo generalInfo;

    StateController sc;
    ActionStateController asc;

    int actionDist;

    bool targetEnemy;
    int unitType;

    void Start()
    {
        map = GameObject.Find("Map").GetComponent<TileMap>();

        thisUnit = GetComponent<Unit>();
        if (this.gameObject.tag == "PlayerUnit" || this.gameObject.tag == "EnemyUnit")
        {
            thisUnitInfo = GetComponent<UnitInfo>();
            unitType = 1;
        }
        else if (this.gameObject.tag == "PlayerGeneral" || this.gameObject.tag == "EnemyGeneral")
        {
            thisGeneralInfo = GetComponent<GeneralInfo>();
            unitType = 2;
        }
        else
        {
            thisBuildingInfo = GetComponent<BuildingInfo>();
            unitType = 3;
        }

        actionDist = 0;
    }

    void Update()
    {
        if (map.selectedUnit != null && unit != map.selectedUnit.GetComponent<Unit>())
        {
            unit = map.selectedUnit.GetComponent<Unit>();
            sc = map.selectedUnit.GetComponent<StateController>();
            asc = map.selectedUnit.GetComponent<ActionStateController>();
        }

        tempX = thisUnit.tileX;
        tempY = thisUnit.tileY;
    }

    void OnMouseUp()
    {
        if (sc != null && sc.state == UnitState.ACTION)
        {
            if (asc.whichAction != 0)
            {
                Debug.Log("Action being done");

                if (asc.whichAction == 1)
                {
                    actionDist = 2;
                    targetEnemy = true;
                }
                else if (asc.whichAction == 2)
                {
                    actionDist = 3;
                    targetEnemy = true;
                }
                else if (asc.whichAction == 3)
                {
                    actionDist = 5;
                    targetEnemy = true;
                }
                else if (asc.whichAction == 4)
                {
                    actionDist = 3;
                    targetEnemy = false;
                }
                else
                {
                    Debug.LogWarning("ActionStateController to ActionClickDetect: whichAction transfer data error");
                }

                map.GeneratePathTo(tempX, tempY, actionDist);

                Node clickTarget = map.graph[tempX, tempY];
                //Debug.Log(clickTarget.x + " " + clickTarget.y);

                List<Unit> allUnits = new List<Unit>();

                foreach (GameObject unit in GameObject.FindGameObjectsWithTag("PlayerUnit"))
                {
                    allUnits.Add(unit.GetComponent<Unit>());
                }

                foreach (GameObject unit in GameObject.FindGameObjectsWithTag("PlayerGeneral"))
                {
                    allUnits.Add(unit.GetComponent<Unit>());
                }

                //Debug.Log(allUnits.Count);
            }
        }
    }
}
