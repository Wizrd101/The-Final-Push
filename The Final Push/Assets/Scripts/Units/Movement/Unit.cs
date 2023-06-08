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

    SpriteRenderer unitModelChild;

    StateController sc;

    Button moveButton;
    Button skipMoveButton;

    ActionStateController asc;

    GameObject camPar;
    Camera cam;
    CameraMove camParMoveScript;

    Canvas moveCv;
    Canvas promptCv;

    public GameObject movementRI;
    GameObject tempMoveRI;

    TurnController tc;

    bool isPlayer;
    bool isBuilding;

    bool triggerOnce = false;
    bool triggerOnce1 = false;
    bool triggerOnce2 = false;

    public bool movable;

    void Start()
    {
        tileX = (int)transform.position.x;
        tileY = (int)transform.position.y;

        if (map == null)
        {
            map = GameObject.FindWithTag("Map").GetComponent<TileMap>();
        }

        unitModelChild = GetComponentInChildren<SpriteRenderer>();
        
        sc = GetComponent<StateController>();

        moveButton = GameObject.Find("MoveButton").GetComponent<Button>();
        skipMoveButton = GameObject.Find("SkipMoveButton").GetComponent<Button>();

        asc = GetComponent<ActionStateController>();

        camPar = GameObject.FindWithTag("CamParent");
        cam = camPar.GetComponentInChildren<Camera>();
        camParMoveScript = camPar.GetComponent<CameraMove>();

        moveCv = GameObject.Find("MoveCanvas").GetComponent<Canvas>();
        promptCv = GameObject.Find("TroopCombatCanvas-ActionPrompt").GetComponent<Canvas>();

        tc = GameObject.Find("TurnController").GetComponent<TurnController>();

        if (this.gameObject.tag == "PlayerUnit" ||  this.gameObject.tag == "PlayerGeneral")
        {
            isPlayer = true;
        }
        else
        {
            isPlayer = false;
        }

        if (this.gameObject.tag == "EnemyBuilding")
        {
            isBuilding = true;
            movable = false;
        }
        else
        {
            isBuilding = false;
        }
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
            skipMoveButton.onClick.AddListener(SkipMove);
        }

        if (!isBuilding)
        {
            // Mostly just reset code that gets the unit moving after the END state
            if (sc.state == UnitState.MOVING)
            {
                // Reseting the Unit's color back to white so it isn't greyscaled anymore
                unitModelChild.color = Color.white;

                // Setting the movable variable to true (so the unit can move, duh)
                movable = true;

                // Code that the enemies don't need
                if (isPlayer)
                {
                    // Reseting the second triggerOnce
                    triggerOnce1 = false;

                    // If this unit is selected, display the Range Indicator for movement
                    if (map.selectedUnit == this.gameObject)
                    {
                        if (!triggerOnce2)
                        {
                            triggerOnce2 = true;
                            tempMoveRI = Instantiate(movementRI, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                            //Debug.Log("tempMoveRI spawned");
                        }
                    }
                    // If not, and tempMoveRI exists, destroy it
                    else if (tempMoveRI)
                    {
                        triggerOnce2 = false;
                        Destroy(tempMoveRI);
                        //Debug.Log("tempMoveRI destroyed");
                    }
                }
            }

            // After the unit is done moving, it automatically triggers the action
            // None of this code is needed if the Unit is an enemy
            if (sc.state == UnitState.ACTION && isPlayer)
            {
                // If the tempMoveRI still exists, we need to destroy it
                if (tempMoveRI)
                {
                    triggerOnce2 = false;
                    Destroy(tempMoveRI);
                    //Debug.Log("tempMoveRI destroyed, but in Action State");
                }

                // Disabling the Move Canvas, so the button doesn't show
                moveCv.enabled = false;

                // Setting the Camera in the right position
                camPar.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, -10f);
                camParMoveScript.zoomState = camParMoveScript.lockZoomState;
                cam.orthographicSize = camParMoveScript.zoomState;
                camParMoveScript.lockCam = true;

                // Trigger Once locks the Action Trigger so it doesn't constantly set the Start Canvas enabled to true
                if (!triggerOnce)
                {
                    triggerOnce = true;
                    asc.ActionTriggerUnit();
                }
            }

            // After the ActionStateController script is done executing, all roads will trigger the end state
            if (sc.state == UnitState.END)
            {
                // Greys out the unit so that it's clear that that unit cannot be interacted with anymore
                unitModelChild.color = Color.grey;

                // Sets the movable variable to false, so the unit cannot move more than once per turn
                movable = false;

                // Unlocks the camera so we can move it again
                camParMoveScript.lockCam = false;

                // Code that the enemies don't need
                if (isPlayer)
                {
                    // Disabling the prompt canvas because we're done with actions
                    promptCv.enabled = false;

                    // Reset TriggerOnce for the next turn
                    triggerOnce = false;

                    // Removes this object from selected object in the TileMap script and disabled the info canvas
                    if (!triggerOnce1)
                    {
                        map.selectedUnit = null;
                        triggerOnce1 = true;
                    }
                }
            }
        }
        else
        {
            if (tc.playerTurn)
            {
                unitModelChild.color = Color.grey;
            }
            else
            {
                unitModelChild.color = Color.white;
            }
        }
    }

    public void MoveNextTile()
    {
        if (currentPath == null || sc.state != UnitState.MOVING)
        {
            return;
        }

        while (currentPath != null)
        {
            currentPath.RemoveAt(0);

            tileX = currentPath[0].x;
            tileY = currentPath[0].y;
            transform.position = map.TileCoordToWorldCoord(tileX, tileY) + new Vector3(0, 0, 0.01f);

            if (currentPath.Count == 1)
            {
                currentPath = null;
            }
        }

        sc.state = UnitState.ACTION;
    }

    public void SkipMove()
    {
        currentPath = null;
        sc.state = UnitState.ACTION;
    }
}
