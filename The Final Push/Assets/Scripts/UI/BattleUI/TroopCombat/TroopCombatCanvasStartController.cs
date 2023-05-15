using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroopCombatCanvasStartController : MonoBehaviour
{
    TurnController TC;
    
    Canvas cvStart;

    public Canvas cvAttack;
    public Canvas cvMagic;

    public Button btnMagic;
    public bool usedMagic;

    void Awake()
    {
        cvStart = GetComponent<Canvas>();
    }

    void Start()
    {
        cvStart.enabled = false;
        if (usedMagic)
        {
            btnMagic.interactable = false;
        }
    }

    public void GoToAttack()
    {
        cvStart.enabled = false;
        cvAttack.enabled = true;
    }

    public void GoToMagic()
    {
        cvStart.enabled = false;
        cvMagic.enabled = true;
    }

    public void EndTurn()
    {
        cvStart.enabled = false;
    }
}
