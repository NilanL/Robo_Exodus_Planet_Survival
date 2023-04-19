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
    Cogling_Miner,
    Main_Base,
    Turret,
    House,
    WallGate,
    orc_Miner,
    Orc_Melee,
    Orc_Range
}

public class Unit_Name : MonoBehaviour
{
    public Unit_Names unit_Name;
}
