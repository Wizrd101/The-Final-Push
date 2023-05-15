using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopCombatCanvasStartAttackController : MonoBehaviour
{
    Canvas cv1Attack;

    void Awake()
    {
        cv1Attack = GetComponent<Canvas>();
    }

    void Start()
    {
        cv1Attack.enabled = false;
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
}
