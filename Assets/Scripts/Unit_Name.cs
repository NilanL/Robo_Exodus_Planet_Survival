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
    Robot_Special,
    Cogling_Melee,
    Cogling_Range,
    Main_Base,
    Turret,
    House,
    WallGate
}

public class Unit_Name : MonoBehaviour
{
    public Unit_Names unit_Name;
}
