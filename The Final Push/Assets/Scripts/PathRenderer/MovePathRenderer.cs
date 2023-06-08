using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePathRenderer : MonoBehaviour
{
    LineRenderer lr;
    List<Node> points = new List<Node>();

    TileMap map;

    StateController tempSC;

    void Start()
    {
        lr = GetComponent<LineRenderer>();

        map = GameObject.Find("Map").GetComponent<TileMap>();
    }

    void Update()
    {
        if (map.currentPath == null && map.selectedUnit != null)
        {
            return;
        }

        tempSC = map.selectedUnit.GetComponent<StateController>();

        if (tempSC.state == UnitState.MOVING)
        {
            points.Clear();
            foreach (Node n in map.currentPath)
            {
                points.Add(n);
            }
            lr.positionCount = points.Count;

            for (int i = 0; i < points.Count; i++)
            {
                lr.SetPosition(i, new Vector3(points[i].x, points[i].y, -0.03f));
            }
        }
    }
}
