using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileMap : MonoBehaviour
{
    Scene curScene;

    public GameObject selectedUnit;

    public TileType[] tileTypes;

    int[,] tiles;
    Node[,] graph;

    // In the for loops, i is x and j is y
    int levelOneMapSizeX = 52;
    int levelOneMapSizeY = 41;

    int levelTwoMapSizeX = 10;
    int levelTwoMapSizeY = 10;

    void Start()
    {
        selectedUnit.GetComponent<Unit>().tileX = (int)selectedUnit.transform.position.x;
        selectedUnit.GetComponent<Unit>().tileY = (int)selectedUnit.transform.position.y;
        selectedUnit.GetComponent<Unit>().map = this;

        curScene = SceneManager.GetActiveScene();

        if (curScene.name == "LevelOneScene")
        {
            GenerateMapDataLevelOne();
            GeneratePathfindingGraphLevelOne();
            GenerateMapVisualsLevelOne();
        }
        else if (curScene.name == "LevelTwoScene")
        {
            GenerateMapDataLevelTwo();
            GeneratePathfindingGraphLevelTwo();
            GenerateMapVisualsLevelTwo();
        }
    }

    void GenerateMapDataLevelOne()
    {
        tiles = new int[levelOneMapSizeX, levelOneMapSizeY];

        for (int i = 0; i < levelOneMapSizeX; i++)
        {
            for (int j = 0; j < levelOneMapSizeY; j++)
            {
                tiles[i, j] = 2;
            }
        }

        for (int i = 2; i <=3; i++)
        {
            for (int j = 17; j <= 23; j++)
            {
                tiles[i, j] = 0;
            }
        }

        for (int i = 4; i <= 50; i++)
        {
            for (int j = 19; j <= 21; j++)
            {
                tiles[i, j] = 0;
            }
        }

        for (int i = 7; i <= 9; i++)
        {
            for (int j = 22; j <= 26; j++)
            {
                tiles[i, j] = 0;
            }
        }

        for (int i = 10; i <= 14; i++)
        {
            for (int j = 27; j <= 29; j++)
            {
                tiles[i, j] = 0;
            }
        }

        for (int i = 15; i <= 26; i++)
        {
            for (int j = 28; j <= 31; j++)
            {
                tiles[i, j] = 0;
            }
        }

        for (int i = 17; i <= 25; i++)
        {
            tiles[i, 32] = 0;
        }

        for (int i = 20; i <= 22; i++)
        {
            for (int j = 34; j <= 36; j++)
            {
                tiles[i, j] = 0;
            }
        }

        for (int i = 21; i <= 23; i++)
        {
            for (int j = 37; j <= 38; j++)
            {
                tiles[i, j] = 0;
            }
        }

        for (int i = 20; i <= 36; i++)
        {
            tiles[i, 27] = 0;
        }

        for (int i = 23; i <= 26; i++)
        {
            tiles[i, 26] = 0;
        }

        for (int i = 22; i <= 24; i++)
        {
            for (int j = 24; j <= 25; j++)
            {
                tiles[i, j] = 0;
            }
        }

        for (int i = 22; i <= 23; i++)
        {
            for (int j = 22; j <= 23; j++)
            {
                tiles[i, j] = 0;
            }
        }

        for (int i = 26; i <= 30; i++)
        {
            for (int j = 28; j <= 30; j++)
            {
                tiles[i, j] = 0;
            }
        }

        for (int i = 31; i <= 32; i++)
        {
            for (int j = 29; j <= 31; j++)
            {
                tiles[i, j] = 0;
            }
        }

        for (int i = 33; i <= 34; i++)
        {
            for (int j = 31; j <= 32; j++)
            {
                tiles[i, j] = 0;
            }
        }

        for (int i = 30; i <= 46; i++)
        {
            for (int j = 24; j <= 26; j++)
            {
                tiles[i, j] = 0;
            }
        }

        for (int i = 45; i <= 47; i++)
        {
            for (int j = 22; j <= 23; j++)
            {
                tiles[i, j] = 0;
            }
        }

        for (int i = 45; i <= 47; i++)
        {
            for (int j = 15; j <= 18; j++)
            {
                tiles[i, j] = 0;
            }
        }

        //4
        tiles[4, 18] = 0;
        tiles[4, 22] = 0;
        //8
        tiles[8, 27] = 0;
        //9
        tiles[9, 27] = 0;
        tiles[9, 28] = 0;
        //10
        tiles[10, 24] = 0;
        tiles[10, 25] = 0;
        tiles[10, 26] = 0;
        //11
        tiles[11, 25] = 0;
        tiles[11, 26] = 0;
        //12
        tiles[12, 30] = 0;
        //13
        tiles[13, 30] = 0;
        //14
        tiles[14, 30] = 0;
        //21
        tiles[21, 22] = 0;
        tiles[21, 33] = 0;
        //22
        tiles[22, 33] = 0;
        tiles[22, 39] = 0;
        //23
        tiles[23, 33] = 0;
        tiles[23, 36] = 0;
        tiles[23, 39] = 0;
        //25
        tiles[25, 25] = 0;
        //28
        tiles[28, 24] = 0;
        //29
        tiles[29, 24] = 0;
        tiles[29, 25] = 0;
        //30
        tiles[30, 31] = 0;
        //32
        tiles[32, 32] = 0;
        //33
        tiles[33, 30] = 0;
        //47
        tiles[47, 24] = 0;
        tiles[47, 25] = 0;
    }

    void GenerateMapDataLevelTwo()
    {
        tiles = new int[levelTwoMapSizeX, levelTwoMapSizeY];

        for (int i = 0; i < levelTwoMapSizeX; i++)
        {
            for (int j = 0; j < levelTwoMapSizeY; j++)
            {
                tiles[i, j] = 0;
            }
        }
    }

    float CostToEnterTile(int x, int y)
    {
        TileType tt = tileTypes[tiles[x, y]];

        return tt.movementCost;
    }

    void GeneratePathfindingGraphLevelOne()
    {
        graph = new Node[levelOneMapSizeX, levelOneMapSizeY];
        for (int i = 0; i < levelOneMapSizeX; i++)
        {
            for (int j = 0; j < levelOneMapSizeY; j++)
            {
                graph[i, j] = new Node();

                graph[i, j].x = i;
                graph[i, j].y = j;
            }
        }

        for (int i = 0; i < levelOneMapSizeX; i++)
        {
            for (int j = 0; j < levelOneMapSizeY; j++)
            {

                //Left
                if (i > 0)
                {
                    graph[i, j].edges.Add(graph[i - 1,j]);
                }
                //Right
                if (i < levelOneMapSizeX - 1)
                {
                    graph[i, j].edges.Add(graph[i + 1, j]);
                }
                //Down
                if (j > 0)
                {
                    graph[i, j].edges.Add(graph[i, j - 1]);
                }
                //Up
                if (j < levelOneMapSizeY - 1)
                {
                    graph[i, j].edges.Add(graph[i, j + 1]);
                }
            }
        }
    }

    void GeneratePathfindingGraphLevelTwo()
    {
        graph = new Node[levelTwoMapSizeX, levelTwoMapSizeY];
        for (int i = 0; i < levelTwoMapSizeX; i++)
        {
            for (int j = 0; j < levelTwoMapSizeY; j++)
            {
                graph[i, j] = new Node();

                graph[i, j].x = i;
                graph[i, j].y = j;
            }
        }

        for (int i = 0; i < levelTwoMapSizeX; i++)
        {
            for (int j = 0; j < levelTwoMapSizeY; j++)
            {

                //Left
                if (i > 0)
                {
                    graph[i, j].edges.Add(graph[i - 1, j]);
                }
                //Right
                if (i < levelTwoMapSizeX - 1)
                {
                    graph[i, j].edges.Add(graph[i + 1, j]);
                }
                //Down
                if (j > 0)
                {
                    graph[i, j].edges.Add(graph[i, j - 1]);
                }
                //Up
                if (j < levelTwoMapSizeY - 1)
                {
                    graph[i, j].edges.Add(graph[i, j + 1]);
                }
            }
        }
    }

    void GenerateMapVisualsLevelOne()
    {
        for (int i = 0; i < levelOneMapSizeX; i++)
        {
            for (int j = 0; j < levelOneMapSizeY; j++)
            {
                GameObject GO = (GameObject)Instantiate(tileTypes[tiles[i, j]].tileVisualPrefab, new Vector3(i, j, 0), Quaternion.identity);
                ClickableTile ct = GO.GetComponent<ClickableTile>();
                ct.tileX = i;
                ct.tileY = j;
                ct.map = this;
            }
        }
    }

    void GenerateMapVisualsLevelTwo()
    {
        for (int i = 0; i < levelTwoMapSizeX; i++)
        {
            for (int j = 0; j < levelTwoMapSizeY; j++)
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

    public bool UnitCanEnterTile(int x, int y)
    {
        return tileTypes[tiles[x,y]].isWalkable;
    }

    public void GeneratePathTo(int x, int y)
    {
        // Teleportation Code (For teleporting a unit anywhere)
        //selectedUnit.GetComponent<Unit>().tileX = x;
        //selectedUnit.GetComponent<Unit>().tileY = y;
        //selectedUnit.transform.position = TileCoordToWorldCoord(x, y);

        // Actual Code (using the currentPath List) (Setting CurrentPath is no longer needed)
        //currentPath = null;

        // Clearing the Old Path
        selectedUnit.GetComponent<Unit>().currentPath = null;

        if (UnitCanEnterTile(x, y) == false)
        {
            return;
        }

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
            // Short and slow solution
            //Node u = unvisited.OrderBy(n => dist[n]).First();

            // More complex, but faster solution
            Node u = null;
            foreach(Node possibleU in unvisited)
            {
                if (u == null || dist[possibleU] < dist[u])
                {
                    u = possibleU;
                }
            }

            unvisited.Remove(u);

            if (u == target)
            {
                break;
            }
            
            foreach(Node v in u.edges)
            {
                //float alt = dist[u] + u.DistanceTo(v);
                float alt = dist[u] + CostToEnterTile(v.x, v.y);

                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }

        if (prev[target] == null)
        {
            return;
        }

        List<Node> currentPath = new List<Node>();

        Node cur = target;

        while (prev[cur] != null)
        {
            currentPath.Add(cur);
            cur = prev[cur];
        }

        currentPath.Reverse();

        selectedUnit.GetComponent<Unit>().currentPath = currentPath;
    }
}
