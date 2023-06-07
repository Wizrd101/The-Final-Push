using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralInfo : MonoBehaviour
{
    // Every General will have a unique quality (extra damage, more range, cool magic, etc.)
    // GeneralType will help us differentiate between them.
    public int generalType = 0;

    // Keep curHealth on 1, because if you don't then update will destroy it immediately.
    public int maxHealth = 15;
    public int curHealth = 1;

    public int atkPower = 3;

    public int moveDist = 7;

    void Start()
    {
        // General 1. Generally better stats.
        if (generalType == 1)
        {
            maxHealth = 20;
            atkPower = 4;
        }
        // General 2. Is tankier than other generals.
        else if (generalType == 2)
        {
            maxHealth = 25;
        }
        // General 3. Will be able to deal more damage than other generals.
        else if (generalType == 3)
        {
            atkPower = 5;
        }
        // General 4. Can automatically use magic once without taking out of the magic total.
        // General 4's ability is handled in the ActionStateController script, bc it's easier.
        // General 5. Can move extra spaces.
        else if (generalType == 5)
        {
            moveDist = 9;
        }
        // Null General, something is wrong. Forgot to set the general's type or something.
        else if (generalType == 0)
        {
            Debug.LogWarning("You forgot to set a General's Type idiot");
        }

        // Sets the current health of the general equal to whatever cap the general has.
        curHealth = maxHealth;
    }

    void Update()
    {
        // Simple death function.
        if (curHealth <= 0)
        {
            Debug.Log("General" + generalType + "Destroyed");
            Destroy(gameObject);
        }
    }
}
