using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour
{
    // Unit Type dictates how strong a Unit is. 1 = 5 troops, 2 = 10 troops.
    // 0 is unset, which is an error, and Generals have their own script.
    public int unitType = 0;
    bool isPlayer;

    public int maxHealth = 1;
    public int curHealth = 1;

    public int atkPower;

    public int moveDist;

    void Start()
    {
        if (this.gameObject.tag == "PlayerUnit")
        {
            isPlayer = true;
        }
        else
        {
            isPlayer = false;
        }

        if (unitType == 1)
        {
            if (isPlayer)
            {
                maxHealth = 10;
            }
            else
            {
                maxHealth = 12;
            }
            atkPower = 2;
        }
        else if (unitType == 2)
        {
            if (isPlayer)
            {
                maxHealth = 20;
            }
            else
            {
                maxHealth = 24;
            }
            atkPower = 4;
        }
        else if (unitType == 0)
        {
            Debug.LogWarning("You forgot to set the Unit Type idiot");
        }

        moveDist = 6;

        curHealth = maxHealth;
    }

    void Update()
    {
        // Simple death function
        if (curHealth <= 0)
        {
            Debug.Log("Unit Destroyed");
            Destroy(gameObject);
        }
    }
}
