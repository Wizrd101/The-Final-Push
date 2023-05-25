using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitSelect : MonoBehaviour
{
    TileMap map;
    Camera cam;

    public LayerMask playerMask;

    void Start()
    {
        if (map == null)
        {
            map = GameObject.FindWithTag("Map").GetComponent<TileMap>();
            cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }
        //playerMask = ~playerMask;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            /*Debug.Log("Mouse Down");
            Vector3 mapPoint = cam.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, -cam.transform.position.z + 0.03f));
            //RaycastHit2D hitInfo = Physics2D.Raycast(cam.transform.position, mapPoint, Mathf.Infinity, playerMask);
            RaycastHit hitInfo;
            hitInfo = Physics.Raycast(cam.transform.position, mapPoint, Mathf.Infinity, playerMask);

            //Debuging statement, change to false when done
            if (true)
            {
                Vector3 tempVector = cam.transform.position;
                Debug.DrawLine(tempVector, mapPoint, Color.red, Mathf.Infinity);
            }

            //if (hitInfo)
            //{
                //Debug.Log("hitInfo error");
            //}
            //else
            //{
            Debug.Log(hitInfo.collider.gameObject.name);
            //}
            if (Physics.Raycast(cam.transform.position, mapPoint, out hitInfo, Mathf.Infinity, playerMask) && hitInfo.transform.gameObject.tag == "PlayerUnit")
            {
                Debug.Log("DONE");
                map.selectedUnit = hitInfo.collider.gameObject;
            }
            else if (Physics.Raycast(cam.transform.position, mapPoint, out hitInfo, Mathf.Infinity, playerMask))
            {
                Debug.Log("hitInfo not detecting");
            }
            else if (hitInfo.transform.gameObject.tag == "PlayerUnit")
            {
                Debug.Log("Raycast Error");
            }*/

            /*Debug.Log("Mouse Clicked");
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            Vector3 temp = cam.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(ray);
            Debug.Log(temp);
            RaycastHit hit;
            Debug.DrawLine(cam.transform.position, temp);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Ray hit something");
                if (hit.collider.tag == "PlayerUnit")
                {
                    Debug.Log("Ray hit a player's unit");
                    map.selectedUnit = hit.collider.gameObject;
                }
                else
                {
                    Debug.Log("Ray hit something else, the player's unit is null");
                    map.selectedUnit = null;
                }
            }*/

            Debug.Log("Clicked");
            RaycastHit2D hit = Physics2D.Raycast(new Vector2 (cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, Mathf.Infinity, playerMask);
            if (hit.collider.tag == "PlayerUnit")
            {
                Debug.Log("A unit has been clicked");
                map.selectedUnit = hit.collider.gameObject;
            }
            else
            {
                Debug.Log("Void");
                map.selectedUnit = null;
            }
        }
    }
}
