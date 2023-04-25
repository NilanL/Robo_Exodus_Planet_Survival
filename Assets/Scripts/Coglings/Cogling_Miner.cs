using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cogling_Miner : MonoBehaviour
{
    // Start is called before the first frame update
    private int maxHealth { get; set; } = 100;
    private int unitAtk { get; set; } = 5;
    private int vehicleAtk { get; set; } = 0;
    private int baseAtk { get; set; } = 0;
    private int unitDef { get; set; } = 5;
    private int vehicleDef { get; set; } = 0;
    private int baseDef { get; set; } = 0;
    private float atkSpd { get; set; } = 1.25f;
    private float miningSpd { get; set; } = 1.3f;
    private int range { get; set; } = 8;

    public GameObject robotMinerPrefab = null;

    public int getMaxHealth()
    {
        return maxHealth;
    }

    public int GetAtk()
    {
        return unitAtk;
    }

    public int GetDef()
    {
        return unitDef;
    }

    public float GetAtkSpeed()
    {
        return atkSpd;
    }

    public int GetRange()
    {
        return range;
    }

    public GameObject GetRobotMinerObject()
    {
        return robotMinerPrefab;
    }
}
