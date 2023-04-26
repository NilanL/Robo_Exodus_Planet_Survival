using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoglingTurretStats : MonoBehaviour
{
    [SerializeField]
    private int maxHealth { get; set; } = 12500;
    [SerializeField]
    private int unitAtk { get; set; } = 10;
    [SerializeField]
    private int vehicleAtk { get; set; } = 0;
    [SerializeField]
    private int baseAtk { get; set; } = 0;
    [SerializeField]
    private int unitDef { get; set; } = 0;
    [SerializeField]
    private int vehicleDef { get; set; } = 0;
    [SerializeField]
    private int baseDef { get; set; } = 0;
    [SerializeField]
    private float atkSpd { get; set; } = 1.2f;
    [SerializeField]
    private float miningSpd { get; set; } = 1.3f;
    [SerializeField]
    private int range { get; set; } = 75;


    [SerializeField]
    public GameObject robotMinerPrefab = null;

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
}
