using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopCombatCanvasStartController : MonoBehaviour
{
    Canvas cv1;

    public Canvas cv1Attack;

    void Awake()
    {
        cv1 = GetComponent<Canvas>();
    }

    void Start()
    {
        cv1.enabled = false;
    }

    public void GoToAttack()
    {
        cv1.enabled = false;
        cv1Attack.enabled = true;
    }

    public void GoToMagic()
    {

    }
}
