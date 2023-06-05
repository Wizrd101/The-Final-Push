using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitInformationDisplay : MonoBehaviour
{
    Canvas dispCanvas;

    RaycastHit2D hit;

    void Awake()
    {
        dispCanvas = GetComponent<Canvas>();

        Physics2D.queriesHitTriggers = true;
    }

    void Start()
    {
        dispCanvas.enabled = false;
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        //Debug.Log("hit outputs: " + Camera.main.transform.position + " " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (hit.collider)
        {
            Debug.Log("Triggered display");
            dispCanvas.enabled = true;

        }
        else
        {

            dispCanvas.enabled = false;
        }
    }
}
