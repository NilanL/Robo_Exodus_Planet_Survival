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
    Cogling_Miner,
    Cogling_Melee,
    Cogling_Range,
    Sleemasi_Miner,
    Sleemasi_Melee,
    Sleemasi_Ranged,
    Graxxian_Miner,
    Graxxian_Melee,
    Graxxian_Ranged,
    Main_Base,
    Robot_Turret,
    Cogling_Turret,
    Sleemasi_Turret,
    Graxxian_Turret,
    House,
    WallGate
}

public class Unit_Name : MonoBehaviour
{
    public Unit_Names unit_Name;
}
