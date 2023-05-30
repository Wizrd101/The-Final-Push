using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitSelect : MonoBehaviour
{
    TileMap map;
    Camera cam;

    void Start()
    {
        if (map == null)
        {
            map = GameObject.FindWithTag("Map").GetComponent<TileMap>();
            cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Select Clicked");
            RaycastHit2D hit = Physics2D.Raycast(new Vector2 (cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, Mathf.Infinity);
            if (hit.collider.tag == "PlayerUnit")
            {
                Debug.Log("A unit has been clicked");
                map.selectedUnit = hit.collider.gameObject;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Unselect Clicked");
            map.selectedUnit = null;
        }

        if (map.selectedUnit == this)
        {
            // Do something to symbolize that this unit is selected
        }
        else
        {
            // Stop doing the thing
        }
    }
}
