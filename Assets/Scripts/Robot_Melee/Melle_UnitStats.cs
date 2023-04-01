using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melle_UnitStats : MonoBehaviour
{
    [SerializeField]
    private int maxHealth { get; set; } = 100;
    [SerializeField]
    private int unitAtk { get; set; } = 8;
    [SerializeField]
    private int vehicleAtk { get; set; } = 0;
    [SerializeField]
    private int baseAtk { get; set; } = 0;
    [SerializeField]
    private int unitDef { get; set; } = 5;
    [SerializeField]
    private int vehicleDef { get; set; } = 0;
    [SerializeField]
    private int baseDef { get; set; } = 0;
    [SerializeField]
    private float atkSpd { get; set; } = 1.5f;
    [SerializeField]
    private float miningSpd { get; set; } = 1.3f;

    [SerializeField]
    public GameObject robotMeleePrefab = null;

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
