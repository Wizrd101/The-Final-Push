using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelect : MonoBehaviour
{
    TileMap map;

    public LayerMask playerMask;

    void Start()
    {
        if (map == null)
        {
            map = GameObject.FindWithTag("Map").GetComponent<TileMap>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Down");
            Vector3 mapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(mapPoint);
            RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.transform.position, mapPoint, Mathf.Infinity, playerMask);
            if (!hitInfo)
            {
                Debug.Log("hitInfo error");
            }
            Debug.Log(hitInfo.collider.gameObject.name);
            if (hitInfo.transform.gameObject.tag == "PlayerUnit")
            {
                map.selectedUnit = hitInfo.collider.gameObject;
            }
        }
    }
}
