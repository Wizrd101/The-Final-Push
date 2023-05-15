using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlemapTroopMovement : MonoBehaviour
{
    Rigidbody2D rb;
    //Animator anim;

    public int squareMoveAmount;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void Move()
    {

    }
}
