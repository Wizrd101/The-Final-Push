using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitInformationDisplay : MonoBehaviour
{
    Canvas dispCanvas;

    TextMeshProUGUI titleText;

    RaycastHit2D hit;

    LayerMask unitLayers;

    void Awake()
    {
        dispCanvas = GetComponent<Canvas>();

        titleText = dispCanvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        Physics2D.queriesHitTriggers = true;
    }

    void Start()
    {
        dispCanvas.enabled = false;

        unitLayers = LayerMask.GetMask("PlayerUnit", "EnemyUnit");
    }

    void Update()
    {
        Vector3 exPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.transform.position, new Vector3(exPos.x, exPos.y, 0f), Mathf.Infinity, unitLayers);
        //Debug.Log("hit outputs: " + Camera.main.transform.position + " " + new Vector3(exPos.x, exPos.y, 0f));
        Debug.DrawLine(Camera.main.transform.position, new Vector3(exPos.x, exPos.y, 0f), Color.blue, 0.5f);
        if (hit.collider)
        {
            Debug.Log(hit.collider.gameObject.name);
            Debug.Log("Triggered display");
            dispCanvas.enabled = true;
            titleText.text = hit.collider.gameObject.name;

        }
        else
        {
            Debug.Log("Untriggered display");
            dispCanvas.enabled = false;
        }
    }
}
