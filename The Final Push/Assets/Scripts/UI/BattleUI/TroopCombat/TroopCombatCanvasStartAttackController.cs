using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroopCombatCanvasStartAttackController : MonoBehaviour
{
    Canvas cvAttack;

    public Canvas cvStart;

    void Awake()
    {
        cvAttack = GetComponent<Canvas>();
    }

    void Start()
    {
        cvAttack.enabled = false;
    }

    public void MeleeAttack()
    {
        /* Melee Attack Info:
         * Strengths: Very strong attack, has a high hit rate and crit chance
         * Weaknesses: Range is absolutely terrible*/
    }

    public void RangedAttack()
    {
        /* Ranged Attack Info:
         * Strengths: Range is amazing, has pretty good damage
         * Weaknesses: Pretty low hit chance, might include an ammo limit*/
    }

    public void Back()
    {
        cvAttack.enabled = false;
        cvStart.enabled = true;
    }
}
