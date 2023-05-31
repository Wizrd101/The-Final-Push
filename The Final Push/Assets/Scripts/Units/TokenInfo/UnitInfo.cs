using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour
{
    // Unit Type dictates how strong a Unit is. 1 = 5 troops, 2 = 10 troops.
    // 0 is unset, which is an error, and Generals have their own script.
    public int unitType = 0;

    int maxHealth = 1;
    public int curHealth = 1;

    public int atkPower;

    public int moveDist;

    void Start()
    {
        if (unitType == 1)
        {
            maxHealth = 10;
            atkPower = 2;
        }
        else if (unitType == 2)
        {
            maxHealth = 20;
            atkPower = 4;
        }
        else if (unitType == 0)
        {
            Debug.LogWarning("You forgot to set the Unit Type idiot");
        }

        moveDist = 5;

        curHealth = maxHealth;
    }

    void Update()
    {
        if (curHealth <= 0)
        {
            Debug.Log("Unit Destroyed");
            Destroy(gameObject);
        }
    }
}
