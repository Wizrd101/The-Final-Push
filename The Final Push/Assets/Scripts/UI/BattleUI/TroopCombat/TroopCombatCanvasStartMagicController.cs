using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroopCombatCanvasStartMagicController : MonoBehaviour
{
    // Magic is an x factor mechanic that can only be used once per battle by the player.
    // All of the units can pull a magic attack or a magic heal, but the generals could have a special magic attack, or maybe only some of them
    
    Canvas cvMagic;

    public Canvas cvStart;

    void Awake()
    {
        cvMagic = GetComponent<Canvas>();
    }

    void Start()
    {
        cvMagic.enabled = false;
    }

    public void MagicAttack()
    {
        /* Magic Attack Info:
         * Strengths: Strong attack, almost garenteed hit chance, and amazing range
         * Weaknesses: Crit chance is nonexistant, can literally only be used once per battle*/
    }

    public void MagicHeal()
    {
        /* Magic Heal Info:
         * Heals one of the player's units within range (which should be pretty short, no more than 2 tiles)
         * Heals extra if the player targets the unit that is using the magic with it*/
    }

    public void Back()
    {
        cvMagic.enabled = false;
        cvStart.enabled = true;
    }
}
