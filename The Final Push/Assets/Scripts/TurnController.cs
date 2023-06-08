using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour
{
    public Canvas winCanvas;
    public Canvas loseCanvas;

    List<StateController> playerSC = new List<StateController>();
    List<StateController> enemySC = new List<StateController>();

    public bool playerTurn;

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

        // We want the player to go first, so we'll set all of the enemy state controllers to END here
        foreach (StateController sc in enemySC)
        {
            sc.state = UnitState.END;
        }
    }

    void Update()
    {
        if (enemySC.Count == 0)
        {
            winCanvas.enabled = true;
        }

        if (playerSC.Count == 0)
        {
            loseCanvas.enabled = true;
        }

        if (playerTurn)
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
                BeginEnemyTurn();
            }
        }
        else
        {
            foreach (StateController sc in enemySC)
            {
                if (sc.state == UnitState.END)
                {
                    counterTC++;
                }
            }

            if (counterTC == enemySC.Count)
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
