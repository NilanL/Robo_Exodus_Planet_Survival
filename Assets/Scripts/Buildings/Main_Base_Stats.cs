using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Base_Stats : MonoBehaviour
{
    [SerializeField]
    private int maxHealth { get; set; } = 20000;
    [SerializeField]
    private int unitAtk { get; set; } = 8;
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
    private float atkSpd { get; set; } = 1.5f;
    [SerializeField]
    private float miningSpd { get; set; } = 1.3f;
    [SerializeField]
    private int range { get; set; } = 10;

    [SerializeField]
    public GameObject robotMinerPrefab = null;

    [SerializeField]
    public GameObject Level2ShipPrefab = null;

    public GameObject getLevel2ShipPrefab()
    {
        return Level2ShipPrefab;
    }

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
