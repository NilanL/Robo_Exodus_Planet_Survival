using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingName
{
    Spaceship,
    TroopCap,
    TroopProd,
    BaseDefense,
    Wall,
    Turret,
    Gate
}

public class Building_Name : MonoBehaviour
{
    public BuildingName buildingName;
}
