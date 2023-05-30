using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public int tileX;
    public int tileY;
    public TileMap map;

    public List<Node> currentPath = null;

    Button moveButton;

    void Start()
    {
        tileX = (int)transform.position.x;
        tileY = (int)transform.position.y;

        if (map == null)
        {
            map = GameObject.FindWithTag("Map").GetComponent<TileMap>();
        }

        moveButton = GameObject.FindWithTag("MoveButton").GetComponent<Button>();
    }

    void Update()
    {
        if (currentPath != null)
        {
            int curNode = 0;

            while (curNode < currentPath.Count - 1)
            {
                Vector3 start = map.TileCoordToWorldCoord(currentPath[curNode].x, currentPath[curNode].y) + new Vector3 (0,0,-1f);
                Vector3 end = map.TileCoordToWorldCoord(currentPath[curNode + 1].x, currentPath[curNode + 1].y) + new Vector3 (0,0,-1f);

                Debug.DrawLine(start, end, Color.red);

                curNode++;
            }
        }

        if (map.selectedUnit == this.gameObject)
        {
            moveButton.onClick.AddListener(MoveNextTile);
        }
    }

    public void MoveNextTile()
    {
        if (currentPath == null)
        {
            return;
        }

        while (currentPath != null)
        {
            currentPath.RemoveAt(0);

            tileX = currentPath[0].x;
            tileY = currentPath[0].y;
            transform.position = map.TileCoordToWorldCoord(tileX, tileY);

            if (currentPath.Count == 1)
            {
                currentPath = null;
            }
        }
    }
}
