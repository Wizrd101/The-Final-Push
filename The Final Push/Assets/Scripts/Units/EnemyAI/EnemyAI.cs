using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    TileMap map;

    StateController sc;

    List<Unit> playerUnits = new List<Unit>();

    List<Unit> possibleMoves = new List<Unit>();

    public int detectionDistance;

    Unit target;
    int lowestMove;

    void Start()
    {
        map = GameObject.FindWithTag("Map").GetComponent<TileMap>();

        sc = GetComponent<StateController>();

        // Setting up the playerUnits list
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("PlayerUnit"))
        {
            playerUnits.Add(unit.GetComponent<Unit>());
        }

        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("PlayerGeneral"))
        {
            playerUnits.Add(unit.GetComponent<Unit>());
        }

        lowestMove = 999999999;
    }

    void Update()
    {
        if (sc.state == UnitState.MOVING)
        {
            TriggerMove();
        }
    }

    IEnumerator TriggerMove()
    {
        foreach (Unit unit in playerUnits)
        {
            map.GeneratePathTo(unit.tileX, unit.tileY, 999999999);
            if (map.currentPath.Count <= detectionDistance)
            {
                possibleMoves.Add(unit);
            }
        }

        if (possibleMoves.Count == 0)
        {
            sc.state = UnitState.END;
            yield break;
        }
        else foreach(Unit unit in possibleMoves)
        {
            map.GeneratePathTo(unit.tileX, unit.tileY, detectionDistance);
            if (map.currentPath.Count < lowestMove)
            {
                target = unit;
            }
        }


        lowestMove = 999999999;

        yield return new WaitForSeconds(.5f);
        TriggerAction();
    }

    IEnumerator TriggerAction()
    {

        yield return new WaitForSeconds(.5f);
        sc.state = UnitState.END;
    }
}
