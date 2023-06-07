using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SingleUnitInfoDisplay : MonoBehaviour
{
    UnitInfo thisUI;
    GeneralInfo thisGI;
    bool isGeneral;

    TileMap map;
    ActionStateController asc;
    
    EnemyAI enemyAI;

    Canvas dispCanvas;

    TextMeshProUGUI titleText;
    TextMeshProUGUI friendlyText;
    TextMeshProUGUI healthText;
    TextMeshProUGUI attackText;
    TextMeshProUGUI moveText;
    TextMeshProUGUI magicText;
    TextMeshProUGUI enemyAmmoText;

    public bool friendly;

    public int unitType;

    void Start()
    {
        if (unitType == 1 || unitType == 2)
        {
            thisUI = GetComponent<UnitInfo>();
            isGeneral = false;
        }
        else if (unitType == 3 || unitType == 4 || unitType == 5 || unitType == 6 || unitType == 7)
        {
            thisGI = GetComponent<GeneralInfo>();
            isGeneral = true;
        }
        else
        {
            Debug.LogWarning("unitType of SingleUnitInfoDisplay not set");
        }

        map = GameObject.FindWithTag("Map").GetComponent<TileMap>();
        asc = GetComponent<ActionStateController>();
        
        if (this.gameObject.tag == "EnemyUnit" || this.gameObject.tag == "EnemyGeneral")
        {
            enemyAI = GetComponent<EnemyAI>();
        }

        dispCanvas = GameObject.Find("InfoDisplayCanvas").GetComponent<Canvas>();
        dispCanvas.enabled = false;

        titleText = dispCanvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        friendlyText = dispCanvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        healthText = dispCanvas.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        attackText = dispCanvas.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        moveText = dispCanvas.transform.GetChild(5).GetComponent<TextMeshProUGUI>();
        magicText = dispCanvas.transform.GetChild(6).GetComponent<TextMeshProUGUI>();
        enemyAmmoText = dispCanvas.transform.GetChild(7).GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (!dispCanvas.enabled && map.selectedUnit == this.gameObject)
        {
            dispCanvas.enabled = true;
            GenerateDisplayInfo();
        }
    }

    void OnMouseOver()
    {
        dispCanvas.enabled = true;

        GenerateDisplayInfo();
    }

    void OnMouseExit()
    {
        dispCanvas.enabled = false;
    }

    void GenerateDisplayInfo()
    {
        if (friendly)
        {
            // 5 troops
            if (unitType == 1)
            {
                titleText.text = "5-troop unit";
            }
            // 10 troops
            else if (unitType == 2)
            {
                titleText.text = "10-troop unit";
            }
            // General 1
            else if (unitType == 3)
            {
                titleText.text = "Osbert Hightower";
            }
            // General 2
            else if (unitType == 4)
            {
                titleText.text = "Wyot Greene";
            }
            // General 3
            else if (unitType == 5)
            {
                titleText.text = "Wulfstan Lintone";
            }
            // General 4
            else if (unitType == 6)
            {
                titleText.text = "Ealdwine Hamptone";
            }
            // General 5
            else if (unitType == 7)
            {
                titleText.text = "Brock Aldene";
            }

            friendlyText.text = "Friendly";
            friendlyText.color = Color.green;

            enemyAmmoText.enabled = false;
        }
        else
        {
            // 5 troops
            if (unitType == 1)
            {
                titleText.text = "5-troop unit";
            }
            // 10 troops
            else if (unitType == 2)
            {
                titleText.text = "10-troop unit";
            }
            // General 1
            else if (unitType == 3)
            {
                titleText.text = "Harold Badcocke";
            }
            // General 2
            else if (unitType == 4)
            {
                titleText.text = "Clive Priestleye";
            }
            // General 3
            else if (unitType == 5)
            {
                titleText.text = "Beowulf Ashleye";
            }
            // General 4
            else if (unitType == 6)
            {
                titleText.text = "Dexter Hytere";
            }
            // General 5
            else if (unitType == 7)
            {
                titleText.text = "Ened Thorne";
            }

            friendlyText.text = "Enemy";
            friendlyText.color = Color.red;

            if (enemyAI.rangedLock)
            {
                enemyAmmoText.enabled = true;
            }
            else
            {
                enemyAmmoText.enabled = false;
            }
        }

        if (unitType == 6 && friendly)
        {
            magicText.enabled = true;
            if (asc.generalFourMagic)
            {
                magicText.text = "Extra magic ready to be used!";
            }
            else
            {
                magicText.text = "Extra magic already used.";
            }
        }
        else
        {
            magicText.enabled = false;
        }

        if (isGeneral)
        {
            healthText.text = "Health: " + thisGI.curHealth + "/" + thisGI.maxHealth;
            attackText.text = "Power: " + thisGI.atkPower;
            moveText.text = "Move Distance: " + thisGI.moveDist;
        }
        else
        {
            healthText.text = "Health: " + thisUI.curHealth + "/" + thisUI.maxHealth;
            attackText.text = "Power: " + thisUI.atkPower;
            moveText.text = "Move Distance: " + thisUI.moveDist;
        }
    }
}
