using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Unit_Names
{
    Miner,
    Wolf,
    Robot_Melee,
    Robot_Ranged,
    Robot_Seige,
    Robot_Special
}

public class Unit_Name : MonoBehaviour
{
    public Unit_Names unit_Name;
}
