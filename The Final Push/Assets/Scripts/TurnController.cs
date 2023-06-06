using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    List<StateController> playerSC = new List<StateController>();
    List<StateController> enemySC = new List<StateController>();

    bool playerTurn;

    int counterTC;

    void Start()
    {
        playerTurn = true;
        
        // Player
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("PlayerUnit"))
        {
            playerSC.Add(unit.GetComponent<StateController>());
        }

        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("PlayerGeneral"))
        {
            playerSC.Add(unit.GetComponent<StateController>());
        }

        // Enemy
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("EnemyUnit"))
        {
            enemySC.Add(unit.GetComponent<StateController>());
        }

        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("EnemyGeneral"))
        {
            enemySC.Add(unit.GetComponent<StateController>());
        }
    }

    void Update()
    {
        foreach (StateController sc in playerSC)
        {
            if (sc.state == UnitState.END)
            {
                counterTC++;
            }
        }

        if (counterTC == playerSC.Count)
        {
            if (playerTurn)
            {
                BeginEnemyTurn();
            }
            else
            {
                BeginPlayerTurn();
            }
        }

        counterTC = 0;
    }

    void BeginPlayerTurn()
    {
        playerTurn = true;

        foreach (StateController sc in playerSC)
        {
            sc.state = UnitState.MOVING;
        }
    }

    void BeginEnemyTurn()
    {
        playerTurn = false;

        foreach (StateController sc in enemySC)
        {
            sc.state = UnitState.MOVING;
        }
    }
}
