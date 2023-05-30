using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitState { MOVING, ACTION, END }

public class StateController : MonoBehaviour
{
    public UnitState state;

    void Start()
    {
        state = UnitState.MOVING;
    }

    
}
