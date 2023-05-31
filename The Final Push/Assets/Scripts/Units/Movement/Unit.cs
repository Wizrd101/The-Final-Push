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

    StateController sc;

    Button moveButton;

    ActionStateController asc;

    GameObject camPar;
    Camera cam;
    CameraMove camParMoveScript;

    bool triggerOnce = false;

    void Start()
    {
        tileX = (int)transform.position.x;
        tileY = (int)transform.position.y;

        if (map == null)
        {
            map = GameObject.FindWithTag("Map").GetComponent<TileMap>();
        }
        
        sc = GetComponent<StateController>();

        moveButton = GameObject.FindWithTag("MoveButton").GetComponent<Button>();

        asc = GetComponent<ActionStateController>();

        camPar = GameObject.FindWithTag("CamParent");
        cam = camPar.GetComponentInChildren<Camera>();
        camParMoveScript = camPar.GetComponent<CameraMove>();

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

        // After the unit is done moving, it automatically triggers the action
        if (sc.state == UnitState.ACTION)
        {
            // Setting the Camera in the right position
            camPar.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, -10f);
            camParMoveScript.zoomState = camParMoveScript.lockZoomState;
            cam.orthographicSize = camParMoveScript.zoomState;
            camParMoveScript.lockCam = true;

            // Trigger Once locks the Action Trigger so it doesn't constantly set the Start Canvas enabled to t
            if (!triggerOnce)
            {
                triggerOnce = true;
                if (this.gameObject.tag == "PlayerUnit")
                {
                    asc.ActionTriggerUnit(this.gameObject);
                }
                else
                {
                    asc.ActionTriggerGeneral(this.gameObject);
                }
            }
        }

        // After the ActionStateController script is done executing, all roads will trigger the end state
        if (sc.state == UnitState.END)
        {
            // Make the Unit grayscaled and un-interactable
            // Reset TriggerOnce for the next turn
            triggerOnce = false;
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
            transform.position = map.TileCoordToWorldCoord(tileX, tileY);

            if (currentPath.Count == 1)
            {
                currentPath = null;
            }
        }

        sc.state = UnitState.ACTION;
    }
}
