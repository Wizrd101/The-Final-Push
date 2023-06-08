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
    public Node[,] graph;

    public List<Node> currentPath;

    // In the for loops, i is x and j is y
    int levelOneMapSizeX = 52;
    int levelOneMapSizeY = 41;

    int levelTwoMapSizeX = 10;
    int levelTwoMapSizeY = 10;

    void Start()
    {
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

    void Update()
    {
        // The P Key resets the path
        if (Input.GetKeyDown(KeyCode.P))
        {
            currentPath = null;
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

        for (int i = 43; i <= 46; i++)
        {
            for (int j = 13; j <= 14; j++)
            {
                tiles[i, j] = 0;
            }
        }

        for (int i = 37; i <= 42; i++)
        {
            for (int j = 11; j <= 13; j++)
            {
                tiles[i, j] = 0;
            }
        }

        for (int i = 40; i <= 41; i++)
        {
            for (int j = 8; j <= 10; j++)
            {
                tiles[i, j] = 0;
            }
        }

        for (int i = 42; i <= 43; i++)
        {
            for (int j = 2; j <= 8; j++)
            {
                tiles[i, j] = 0;
            }
        }

        for (int i = 44; i <= 45; i++)
        {
            for (int j = 1; j <= 2; j++)
            {
                tiles[i, j] = 0;
            }
        }

        for (int i = 32; i <= 35; i++)
        {
            for (int j = 11; j <= 14; j++)
            {
                tiles[i, j] = 0;
            }
        }

        for (int i = 31; i <= 33; i++)
        {
            for (int j = 15; j <= 18; j++)
            {
                tiles[i, j] = 0;
            }
        }

        for (int i = 22; i <= 32; i++)
        {
            for (int j = 9; j <= 10; j++)
            {
                tiles[i, j] = 0;
            }
        }

        for (int i = 25; i <= 28; i++)
        {
            tiles[i, 8] = 0;
        }

        for (int i = 20; i <= 27; i++)
        {
            for (int j = 11; j <= 12; j++)
            {
                tiles[i, j] = 0;
            }
        }

        for (int i = 17; i <= 23; i++)
        {
            for (int j = 13; j <= 14; j++)
            {
                tiles[i, j] = 0;
            }
        }

        for (int i = 15; i <= 19; i++)
        {
            for (int j = 15; j <= 16; j++)
            {
                tiles[i, j] = 0;
            }
        }

        for (int i = 13; i <= 15; i++)
        {
            for (int j = 17; j <= 18; j++)
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
        tiles[14, 16] = 0;
        tiles[14, 30] = 0;
        //16
        tiles[16, 14] = 0;
        tiles[16, 17] = 0;
        //19
        tiles[19, 12] = 0;
        //20
        tiles[20, 15] = 0;
        //21
        tiles[21, 10] = 0;
        tiles[21, 15] = 0;
        tiles[21, 22] = 0;
        tiles[21, 33] = 0;
        //22
        tiles[22, 33] = 0;
        tiles[22, 39] = 0;
        //23
        tiles[23, 33] = 0;
        tiles[23, 36] = 0;
        tiles[23, 39] = 0;
        //24
        tiles[24, 13] = 0;
        //25
        tiles[25, 25] = 0;
        //28
        tiles[28, 11] = 0;
        tiles[28, 24] = 0;
        //29
        tiles[29, 24] = 0;
        tiles[29, 25] = 0;
        //30
        tiles[30, 31] = 0;
        //31
        tiles[31, 11] = 0;
        //32
        tiles[32, 32] = 0;
        //33
        tiles[33, 10] = 0;
        tiles[33, 30] = 0;
        //34
        tiles[34, 10] = 0;
        tiles[34, 15] = 0;
        //36
        tiles[36, 12] = 0;
        tiles[36, 13] = 0;
        //41
        tiles[41, 7] = 0;
        //42
        tiles[42, 9] = 0;
        tiles[42, 14] = 0;
        //43
        tiles[43, 1] = 0;
        tiles[43, 12] = 0;
        //44
        tiles[44, 3] = 0;
        tiles[44, 12] = 0;
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

    public void GeneratePathTo(int x, int y, int maxDist)
    {
        // Teleportation Code (For teleporting a unit anywhere)
        //selectedUnit.GetComponent<Unit>().tileX = x;
        //selectedUnit.GetComponent<Unit>().tileY = y;
        //selectedUnit.transform.position = TileCoordToWorldCoord(x, y);

        // If there is no selected unit, there is no path to generate
        if (selectedUnit == null)
        {
            return;
        }

        // Clearing the Old Path
        selectedUnit.GetComponent<Unit>().currentPath = null;

        // If the Unit cannot enter the tile we clicked on (like a mountain) return out
        if (UnitCanEnterTile(x, y) == false)
        {
            return;
        }

        // Setting two Dictionaries of Nodes...
        // dist is the amount of distance between two nodes, which prev is all of the nodes that we have visited
        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        // ...And a list of unvisited Nodes
        List<Node> unvisited = new List<Node>();

        // The Source Node is where the selected Unit is
        Node source = graph[selectedUnit.GetComponent<Unit>().tileX, selectedUnit.GetComponent<Unit>().tileY];

        // The target is a Node on the list of Nodes graph
        Node target = graph[x, y];

        // resets the current dist to null (because we're starting at the start), and prev to empty, so that we can fill it again.
        dist[source] = 0;
        prev[source] = null;

        // Simple loop that sets the distance to every node to infinity and adds it to the list of unvisited nodes
        foreach(Node v in graph)
        {
            if (v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }

            unvisited.Add(v);
        }

        // While loop that will trigger as long as the pathfinding hasn't hit every node yet
        while(unvisited.Count > 0)
        {
            // Short and slow solution
            //Node u = unvisited.OrderBy(n => dist[n]).First();

            // More complex, but faster solution
            // Temporarily sets the node to null
            Node u = null;
            // Detects that null node
            foreach(Node possibleU in unvisited)
            {
                if (u == null || dist[possibleU] < dist[u])
                {
                    u = possibleU;
                }
            }

            // Remove the node from the unvisited list, because we just visited it
            unvisited.Remove(u);

            // If u is where we want to go, we can stop the while loop
            if (u == target)
            {
                break;
            }
            
            // Assigns a cost to each way that a unit can move in a tile
            foreach(Node v in u.edges)
            {
                float alt = dist[u] + CostToEnterTile(v.x, v.y);

                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }

        // If we hit the target, stop
        if (prev[target] == null)
        {
            return;
        }

        // Sets the currentPath equal to a list of nodes
        currentPath = new List<Node>();

        // Sets a temporary cur value for our target
        Node cur = target;

        // While the path hasn't run out of previous options, add another one and reset the prev Dictionary
        while (prev[cur] != null)
        {
            currentPath.Add(cur);
            cur = prev[cur];
        }

        // Adds the source, which is required if only moving one square is desired
        currentPath.Add(source);

        // Flips the path of nodes around, so that we go to the target, not from the target
        currentPath.Reverse();

        Debug.Log("Path Length: " + currentPath.Count);
        
        if (currentPath.Count > maxDist)
        {
            Debug.Log("Point Out of Range");
            currentPath = null;
            return;
        }

        selectedUnit.GetComponent<Unit>().currentPath = currentPath;
    }
}
