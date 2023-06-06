using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCanvasController : MonoBehaviour
{
    Canvas moveCanvas;

    TileMap map;

    int counter;

    void Start()
    {
        moveCanvas = GetComponent<Canvas>();
        map = GameObject.Find("Map").GetComponent<TileMap>();
    }

    void Update()
    {
        List<StateController> allSC = new List<StateController>();

        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("PlayerUnit"))
        {
            allSC.Add(unit.GetComponent<StateController>());
        }

        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("PlayerGeneral"))
        {
            allSC.Add(unit.GetComponent<StateController>());
        }

        foreach (StateController sc in allSC)
        {
            if (sc.state == UnitState.ACTION)
            {
                counter++;
            }
        }

        if (counter == 0)
        {
            if (map.selectedUnit == null)
            {
                moveCanvas.enabled = false;
            }
            else
            {
                moveCanvas.enabled = true;
            }
        }

        counter = 0;
    }
}
