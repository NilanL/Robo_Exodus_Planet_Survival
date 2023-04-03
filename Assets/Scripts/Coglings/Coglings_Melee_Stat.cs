using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coglings_Melee_Stat : MonoBehaviour
{
    private int maxHealth { get; set; } = 600;
    private int unitAtk { get; set; } = 15;
    private int vehicleAtk { get; set; } = 0;
    private int baseAtk { get; set; } = 0;
    private int unitDef { get; set; } = 5;
    private int vehicleDef { get; set; } = 0;
    private int baseDef { get; set; } = 0;
    private float atkSpd { get; set; } = 1.25f;
    private float miningSpd { get; set; } = 1.3f;
    private int range { get; set; } = 8;

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
}
