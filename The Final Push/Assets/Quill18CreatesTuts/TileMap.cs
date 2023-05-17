using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    public GameObject selectedUnit;

    public TileType[] tileTypes;

    int[,] tiles;
    Node[,] graph;

    int mapSizeX = 10;
    int mapSizeY = 10;

    void Start()
    {
        GenerateMapData();
        GeneratePathfindingGraph();
        GenerateMapVisuals();
    }

    void GenerateMapData()
    {
        tiles = new int[mapSizeX, mapSizeY];

        for (int i = 0; i < mapSizeX; i++)
        {
            for (int j = 0; j < mapSizeY; j++)
            {
                tiles[i, j] = 0;
            }
        }
    }

    class Node
    {
        public List<Node> edges;
        public int x;
        public int y;

        public Node()
        {
            edges = new List<Node>();
        }

        public float DistanceTo(Node n)
        {
            return Vector2.Distance(new Vector2(x,y), new Vector2(n.x, n.y));
        }
    }

    void GeneratePathfindingGraph()
    {
        graph = new Node[mapSizeX, mapSizeY];
        for (int i = 0; i < mapSizeX; i++)
        {
            for (int j = 0; j < mapSizeY; j++)
            {
                graph[i, j].x = i;
                graph[i, j].y = j;

                //Left
                if (i > 0)
                {
                    graph[i, j].edges.Add(graph[i - 1,j]);
                }
                //Right
                if (i < mapSizeX - 1)
                {
                    graph[i, j].edges.Add(graph[i + 1, j]);
                }
                //Down
                if (j > 0)
                {
                    graph[i, j].edges.Add(graph[i, j - 1]);
                }
                //Up
                if (j < mapSizeY - 1)
                {
                    graph[i, j].edges.Add(graph[i, j + 1]);
                }
            }
        }
    }

    void GenerateMapVisuals()
    {
        for (int i = 0; i < mapSizeX; i++)
        {
            for (int j = 0; j < mapSizeY; j++)
            {
                GameObject GO = (GameObject)Instantiate(tileTypes[tiles[i, j]].tileVisualPrefab, new Vector3(i, j, 0), Quaternion.identity);
                ClickableTile ct = GO.GetComponent<ClickableTile>();
                ct.tileX = i;
                ct.tileY = j;
                ct.map = this;
            }
        }
    }
    
    public Vector3 TileCoordToWorldCoord(int x, int y)
    {
        return new Vector3(x, y, 0);
    }

    public void MoveSelectedUnitTo(int x, int y)
    {
        //selectedUnit.GetComponent<Unit>().tileX = x;
        //selectedUnit.GetComponent<Unit>().tileY = y;
        //selectedUnit.transform.position = TileCoordToWorldCoord(x, y);

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        List<Node> unvisited = new List<Node>();

        Node source = graph[selectedUnit.GetComponent<Unit>().tileX, selectedUnit.GetComponent<Unit>().tileY];

        Node target = graph[x, y];

        dist[source] = 0;
        prev[source] = null;

        foreach(Node v in graph)
        {
            if (v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }

            unvisited.Add(v);
        }

        while(unvisited.Count > 0)
        {
            Node u = unvisited.OrderBy(n => dist[n]).First();
            unvisited.Remove(u);
            
            foreach(Node v in u.edges)
            {
                float alt = dist[u] + u.DistanceTo(v);
                
                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }

    }
}
