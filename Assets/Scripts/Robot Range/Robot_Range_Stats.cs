using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_Range_Stats : MonoBehaviour
{
    [SerializeField]
    private int maxHealth { get; set; } = 120;
    [SerializeField]
    private int unitAtk { get; set; } = 7;
    [SerializeField]
    private int vehicleAtk { get; set; } = 0;
    [SerializeField]
    private int baseAtk { get; set; } = 0;
    [SerializeField]
    private int unitDef { get; set; } = 8;
    [SerializeField]
    private int vehicleDef { get; set; } = 0;
    [SerializeField]
    private int baseDef { get; set; } = 0;
    [SerializeField]
    private float atkSpd { get; set; } = 2.2f;
    [SerializeField]
    private float miningSpd { get; set; } = 1.3f;
    [SerializeField]
    private int range { get; set; } = 45;

    [SerializeField]
    public GameObject robotMeleePrefab = null;

    public int getRange()
    {
        return range;
    }

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

    public GameObject GetRobotMinerObject()
    {
        return robotMeleePrefab;
    }
}
