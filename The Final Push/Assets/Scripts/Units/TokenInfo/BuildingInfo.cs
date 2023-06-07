using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInfo : MonoBehaviour
{
    public bool isPrison;

    public int maxHealth = 1;
    public int curHealth = 1;

    public GameObject prisonerPref;

    void Start()
    {
        if (!isPrison)
        {
            maxHealth = 30;
        }
        else
        {
            maxHealth = 40;
        }

        curHealth = maxHealth;
    }

    void Update()
    {
        // Simple death function
        if (curHealth <= 0)
        {
            Debug.Log("Building Destroyed");

            // If the building was a prison, the prisoners are freed upon it's fall
            if (isPrison)
            {
                Instantiate(prisonerPref, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            }
            
            Destroy(gameObject);
        }
    }
}
