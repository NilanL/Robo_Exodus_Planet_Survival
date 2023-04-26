using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
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
    public float atkSpd { get; set; } = 1.5f;
    [SerializeField]
    private float miningSpd { get; set; } = 1.3f;
    [SerializeField]
    private int range { get; set; } = 10;

    [SerializeField]
    public GameObject Prefab = null;

    GameObject gm;
    Unit_Name name;

    void Start()
    {
        gm = GameObject.Find("GameManager");
        name = GetComponent<Unit_Name>();
        initializestats();
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

    public GameObject GetRobotMinerObject()
    {
        return Prefab;
    }

    public int setRange(int r)
    {
        range = r;
        return range;
    }

    public int setMaxHealth(int h)
    {
        maxHealth = h;
        return maxHealth;
    }

    public int setAtk(int a)
    {
        unitAtk = a;
        return unitAtk;
    }

    public int setDef(int d)
    {
        unitDef = d;
        return unitDef;
    }

    public float setAtkSpeed(float a)
    {
        atkSpd = a;
        return atkSpd;
    }

    public GameObject setObject(GameObject g)
    {
        g = Prefab;
        return Prefab;
    }

    public void InitializeUpdate()
    {
        switch (name.unit_Name)
        {
            case Unit_Names.Miner:
                initializeMiner();
                break;
            case Unit_Names.Robot_Melee:
                Initializemeleeunit();
                break;
            case Unit_Names.Robot_Ranged:
                InitializeRangeUnit();
                break;
        }
    }


    private void initializestats()
    {
        switch(name.unit_Name)
        {
            case Unit_Names.Miner:
                initializeMiner();
                break;
            case Unit_Names.Robot_Melee:
                Initializemeleeunit();
                break;
            case Unit_Names.Robot_Ranged:
                InitializeRangeUnit();
                break;
            case Unit_Names.Wolf:
                InitializeWolf();
                break;
            case Unit_Names.Cogling_Melee:
                InitializeCoglingMeleeUnit();
                break;
            case Unit_Names.Cogling_Range:
                InitializeCoglingRangeUnit();
                break;
            case Unit_Names.Cogling_Miner:
                InitializeCoglingMinerUnit();
                break;
            case Unit_Names.Main_Base:
                InitializeMainBaseBuilding();
                break;
            case Unit_Names.House:
                InitializeHouseBuilding();
                break;
            case Unit_Names.Robot_Turret:
                InitializeRobotTurretBuilding();
                break;
            case Unit_Names.Cogling_Turret:
                InitializeCoglingTurretBuilding();
                break;
            case Unit_Names.Sleemasi_Turret:
                InitializeSleemasiTurretBuilding();
                break;
            case Unit_Names.Graxxian_Turret:
                InitializeGraxxianTurretBuilding();
                break;
            case Unit_Names.WallGate:
                InitializeWallGateBuilding();
                break;
            case Unit_Names.Graxxian_Miner:
                InitializeOrcMiner();
                break;
            case Unit_Names.Graxxian_Melee:
                InitializeOrcMelee();
                break;
            case Unit_Names.Graxxian_Ranged:
                InitializeOrcRange();
                break;
        }
    }

    private void initializeMiner()
    {
        var minerstats = gm.GetComponent<MinerStats>();
        maxHealth = minerstats.getMaxHealth();
        unitAtk = minerstats.GetAtk();
        unitDef = minerstats.GetDef();
        atkSpd = minerstats.GetAtkSpeed();
        range = minerstats.getRange();
        
    }

    private void InitializeWolf()
    {
        var minerstats = gm.GetComponent<Wolf_Stats>();
        maxHealth = minerstats.getMaxHealth();
        unitAtk = minerstats.GetAtk();
        unitDef = minerstats.GetDef();
        atkSpd = minerstats.GetAtkSpeed();
        range = minerstats.GetRange();
    }

    private void Initializemeleeunit()
    {
        var minerstats = gm.GetComponent<Melle_UnitStats>();
        maxHealth = minerstats.getMaxHealth();
        unitAtk = minerstats.GetAtk();
        unitDef = minerstats.GetDef();
        atkSpd = minerstats.GetAtkSpeed();
        range = minerstats.getRange();
    }

    private void InitializeRangeUnit()
    {
        var minerstats = gm.GetComponent<Robot_Range_Stats>();
        maxHealth = minerstats.getMaxHealth();
        unitAtk = minerstats.GetAtk();
        unitDef = minerstats.GetDef();
        atkSpd = minerstats.GetAtkSpeed();
        range = minerstats.getRange();
    }

    private void InitializeCoglingMeleeUnit()
    {
        var minerstats = gm.GetComponent<Coglings_Melee_Stat>();
        maxHealth = minerstats.getMaxHealth();
        unitAtk = minerstats.GetAtk();
        unitDef = minerstats.GetDef();
        atkSpd = minerstats.GetAtkSpeed();
        range = minerstats.GetRange();
    }

    private void InitializeCoglingMinerUnit()
    {
        var minerstats = gm.GetComponent<Cogling_Miner>();
        maxHealth = minerstats.getMaxHealth();
        unitAtk = minerstats.GetAtk();
        unitDef = minerstats.GetDef();
        atkSpd = minerstats.GetAtkSpeed();
        range = minerstats.GetRange();
    }

    private void InitializeCoglingRangeUnit()
    {
        var minerstats = gm.GetComponent<Coglings_Range_Stat>();
        maxHealth = minerstats.getMaxHealth();
        unitAtk = minerstats.GetAtk();
        unitDef = minerstats.GetDef();
        atkSpd = minerstats.GetAtkSpeed();
        range = minerstats.GetRange();
    }

    private void InitializeMainBaseBuilding()
    {
        var minerstats = gm.GetComponent<Main_Base_Stats>();
        maxHealth = minerstats.getMaxHealth();
        unitAtk = minerstats.GetAtk();
        unitDef = minerstats.GetDef();
        atkSpd = minerstats.GetAtkSpeed();
    }

    private void InitializeHouseBuilding()
    {
        var minerstats = gm.GetComponent<House_Stats>();
        maxHealth = minerstats.getMaxHealth();
        unitAtk = minerstats.GetAtk();
        unitDef = minerstats.GetDef();
        atkSpd = minerstats.GetAtkSpeed();
    }

    private void InitializeRobotTurretBuilding()
    {
        var minerstats = gm.GetComponent<RobotTurretStats>();
        maxHealth = minerstats.getMaxHealth();
        unitAtk = minerstats.GetAtk();
        unitDef = minerstats.GetDef();
        atkSpd = minerstats.GetAtkSpeed();
        range = minerstats.getRange();
    }

    private void InitializeCoglingTurretBuilding()
    {
        var minerstats = gm.GetComponent<CoglingTurretStats>();
        maxHealth = minerstats.getMaxHealth();
        unitAtk = minerstats.GetAtk();
        unitDef = minerstats.GetDef();
        atkSpd = minerstats.GetAtkSpeed();
        range = minerstats.getRange();
    }

    private void InitializeGraxxianTurretBuilding()
    {
        var minerstats = gm.GetComponent<GraxxianTurretStats>();
        maxHealth = minerstats.getMaxHealth();
        unitAtk = minerstats.GetAtk();
        unitDef = minerstats.GetDef();
        atkSpd = minerstats.GetAtkSpeed();
        range = minerstats.getRange();
    }

    private void InitializeSleemasiTurretBuilding()
    {
        var minerstats = gm.GetComponent<SleemasiTurretStats>();
        maxHealth = minerstats.getMaxHealth();
        unitAtk = minerstats.GetAtk();
        unitDef = minerstats.GetDef();
        atkSpd = minerstats.GetAtkSpeed();
        range = minerstats.getRange();
    }

    private void InitializeWallGateBuilding()
    {
        var minerstats = gm.GetComponent<WallGate_Stats>();
        maxHealth = minerstats.getMaxHealth();
        unitAtk = minerstats.GetAtk();
        unitDef = minerstats.GetDef();
        atkSpd = minerstats.GetAtkSpeed();
    }

    private void InitializeOrcMiner()
    {
        var minerstats = gm.GetComponent<OrcMinerStats>();
        maxHealth = minerstats.getMaxHealth();
        unitAtk = minerstats.GetAtk();
        unitDef = minerstats.GetDef();
        atkSpd = minerstats.GetAtkSpeed();
    }

    private void InitializeOrcMelee()
    {
        var minerstats = gm.GetComponent<OrcMeleeStats>();
        maxHealth = minerstats.getMaxHealth();
        unitAtk = minerstats.GetAtk();
        unitDef = minerstats.GetDef();
        atkSpd = minerstats.GetAtkSpeed();
    }

    private void InitializeOrcRange()
    {
        var minerstats = gm.GetComponent<OrcRangeStats>();
        maxHealth = minerstats.getMaxHealth();
        unitAtk = minerstats.GetAtk();
        unitDef = minerstats.GetDef();
        atkSpd = minerstats.GetAtkSpeed();
        range = minerstats.GetRange();
    }
}
