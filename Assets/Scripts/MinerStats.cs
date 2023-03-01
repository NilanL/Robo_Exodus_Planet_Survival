using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerStats : MonoBehaviour
{

    private int maxHealth { get; set; } = 100;
    private int unitAtk { get; set; } = 8;
    private int vehicleAtk { get; set; } = 0;
    private int baseAtk { get; set; } = 0;
    private int unitDef { get; set; } = 5;
    private int vehicleDef { get; set; } = 0;
    private int baseDef { get; set; } = 0;
    private float atkSpd { get; set; } = 1.5f;
    private float miningSpd { get; set; } = 1.3f;

    [SerializeField]
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

    public GameObject GetRobotMinerObject()
    {
        return robotMinerPrefab;
    }

}
