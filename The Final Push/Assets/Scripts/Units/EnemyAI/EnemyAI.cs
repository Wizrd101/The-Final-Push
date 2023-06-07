using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    TileMap map;

    Unit thisUnit;
    StateController sc;
    ActionStateController asc;

    GameObject camPar;
    Camera cam;
    CameraMove camMove;
    bool camFollow;

    List<Unit> playerUnits = new List<Unit>();

    List<Unit> possibleMoves = new List<Unit>();

    public int detectionDistance;

    Unit target;
    int lowestMove;

    public bool rangedLock;

    void Start()
    {
        map = GameObject.FindWithTag("Map").GetComponent<TileMap>();

        thisUnit = GetComponent<Unit>();
        sc = GetComponent<StateController>();
        asc = GetComponent<ActionStateController>();

        camPar = GameObject.FindWithTag("CamParent");
        cam = camPar.GetComponentInChildren<Camera>();
        camMove = camPar.GetComponent<CameraMove>();
        camFollow = false;

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

        rangedLock = false;
    }

    void Update()
    {
        if (sc.state == UnitState.MOVING)
        {
            TriggerMove();
        }

        if (camFollow)
        {
            camPar.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10f);
            camMove.zoomState = camMove.lockZoomState;
            cam.orthographicSize = camMove.zoomState;
            camMove.lockCam = true;
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

        // If there are no moves, break out of this for loop
        if (possibleMoves.Count == 0)
        {
            CleanupTurn();
            yield break;
        }
        // If there are moves, target the closest one
        else foreach(Unit unit in possibleMoves)
        {
                camFollow = true;
            map.GeneratePathTo(unit.tileX, unit.tileY, detectionDistance);
            if (map.currentPath.Count < lowestMove)
            {
                target = unit;
            }
        }

        // Action code, but I can't put this in the for loop or it would execute over and over
        if (target != null)
        {
            map.GeneratePathTo(target.tileX, target.tileY, detectionDistance);
            thisUnit.MoveNextTile();

            sc.state = UnitState.ACTION;
        }

        lowestMove = 999999999;

        yield return new WaitForSeconds(.5f);
        TriggerAction();
    }

    IEnumerator TriggerAction()
    {
        // If the enemy is one unit away, attack them with a melee attack
        if (map.currentPath.Count == 2)
        {
            asc.EnemyMeleeAttack(target);
        }
        // If the unit is two units away, attack them with a ranged attack
        // This attack can only be done once, and there's a one way lock in the if statement
        else if (map.currentPath.Count == 3 && !rangedLock)
        {
            rangedLock = true;
            asc.EnemyRangedAttack(target);
        }

        yield return new WaitForSeconds(.5f);
        CleanupTurn();
    }

    void CleanupTurn()
    {
        target = null;
        possibleMoves.Clear();
        camFollow = false;
        sc.state = UnitState.END;
    }
}
