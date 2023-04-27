using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditBuilding : MonoBehaviour
{
    private GameObject gm;
    private GameManager gameManager;
    private GameObject UI;
    private Text textField;

    public GameObject melee;
    public GameObject ranged;
    public GameObject miner;

    private void Start()
    {
        gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
        UI = GameObject.Find("UI");
        melee = gm.GetComponent<Melle_UnitStats>().robotMeleePrefab;
        ranged = gm.GetComponent<Robot_Range_Stats>().robotMeleePrefab;
        miner = gm.GetComponent<MinerStats>().robotMinerPrefab;
        textField = GameObject.Find("Troops").GetComponent<Text>();
    }

    void Update()
    {
        UpdateTroopCount();
    }

    public void UpdateTroopCount()
    {
        textField.text = "Troops " + gameManager.Unit_count + "/" + gameManager.Max_Unit_Count;
    }

    public void RepairTurret()
    {
        var turret = gm.GetComponent<GameManager>().ActiveTurret;
        if (turret)
        {
            var turretHealth = turret.GetComponent<Unit_Health>();
            if (gameManager.Ironite >= 100 && turretHealth.currentHealth != turretHealth.maxHealth)
            {
                gameManager.Ironite -= 100;

                int newHealth = turretHealth.currentHealth + 1000;
                turretHealth.currentHealth = (newHealth > turretHealth.maxHealth) ? turretHealth.maxHealth : newHealth;

                var window = UI.transform.Find("Building Windows/Turret Window").gameObject;
                var health = window.transform.Find("Turret Health").gameObject;

                health.GetComponent<Text>().text = "Health: (" + turretHealth.currentHealth + "/" + turretHealth.maxHealth + ")";
            }
        }
    }

    public void DestroyTurret()
    {
        gm.GetComponent<GameManager>().DecrementTurretCount();
        var turret = gm.GetComponent<GameManager>().ActiveTurret;
        Destroy(turret);
    }

    public void RepairWall()
    {
        var wall = gm.GetComponent<GameManager>().ActiveWall;

        if (wall)
        {
            var wallHealth = wall.GetComponent<Unit_Health>();

            if (gameManager.Ironite >= 100 && wallHealth.currentHealth != wallHealth.maxHealth)
            {
                gameManager.Ironite -= 100;

                int newHealth = wallHealth.currentHealth + 1000;
                wallHealth.currentHealth = (newHealth > wallHealth.maxHealth) ? wallHealth.maxHealth : newHealth;

                var window = UI.transform.Find("Building Windows/Wall Window").gameObject;
                var health = window.transform.Find("Wall Health").gameObject;

                health.GetComponent<Text>().text = "Health: (" + wallHealth.currentHealth + "/" + wallHealth.maxHealth + ")";
            }
        }
    }

    public void DestroyWall()
    {
        //var turret = gm.GetComponent<GameManager>().ActiveTurret;
        //Destroy(turret);
    }

    public void RepairGate()
    {
        var gate = gm.GetComponent<GameManager>().ActiveGate;
        if (gate)
        {
            var gateHealth = gate.GetComponent<Unit_Health>();
            if (gameManager.Ironite >= 100 && gateHealth.currentHealth != gateHealth.maxHealth)
            {
                gameManager.Ironite -= 100;

                int newHealth = gateHealth.currentHealth + 1000;
                gateHealth.currentHealth = (newHealth > gateHealth.maxHealth) ? gateHealth.maxHealth : newHealth;

                var window = UI.transform.Find("Building Windows/Gate Window").gameObject;
                var health = window.transform.Find("Gate Health").gameObject;

                health.GetComponent<Text>().text = "Health: (" + gateHealth.currentHealth + "/" + gateHealth.maxHealth + ")";
            }
        }
    }

    public void OpenGate()
    {
        var gate = gm.GetComponent<GameManager>().ActiveGate;
        var phase = gate.transform.GetChild(gate.transform.childCount - 1).gameObject;
        phase.SetActive(false);
    }

    public void CloseGate()
    {
        var gate = gm.GetComponent<GameManager>().ActiveGate;
        var phase = gate.transform.GetChild(gate.transform.childCount - 1).gameObject;
        phase.SetActive(true);
    }

    public void CreateMinerUnit()
    {
        if (gameManager.Ironite >= 100 && gameManager.Unit_count < gameManager.Max_Unit_Count)
        {
            gameManager.Ironite -= 100;

            var baseObj = GameObject.Find("Robot_Base_Level1");
            if (baseObj == null)
            {
                baseObj = GameObject.Find("Robot_Base_Level2");
            }

            var spawnLoc = baseObj.transform.Find("Spawn_Location");

            //var prod = gameManager.ActiveTroopProd;
            //var spawnLoc = prod.transform.GetChild(prod.transform.childCount - 1);

            gameManager.Unit_count += 1;

            var unit = Instantiate(miner, spawnLoc.position, spawnLoc.rotation);

            gameManager.selectables.Add(unit);
            //gameManager.unitsList.Add(unit);
        }
    }

    public void CreateMeleeUnit()
    {
        if (gameManager.Ironite >= 250 && gameManager.Unit_count < gameManager.Max_Unit_Count)
        {
            gameManager.Ironite -= 250;
            var prod = gameManager.ActiveTroopProd;
            var spawnLoc = prod.transform.GetChild(prod.transform.childCount - 1);

            gameManager.Unit_count += 1;

            var unit = Instantiate(melee, spawnLoc.position, spawnLoc.rotation);

            gameManager.selectables.Add(unit);
            //gameManager.unitsList.Add(unit);
        }
    }

    public void CreateRangedUnit()
    {
        if (gameManager.Ironite >= 250 && gameManager.Unit_count < gameManager.Max_Unit_Count)
        {
            gameManager.Ironite -= 250;
            var prod = gameManager.ActiveTroopProd;
            var spawnLoc = prod.transform.GetChild(prod.transform.childCount - 1);

            gameManager.Unit_count += 1;

            var unit = Instantiate(ranged, spawnLoc.position, spawnLoc.rotation);

            gameManager.selectables.Add(unit);
            //gameManager.unitsList.Add(unit);
        }
    }
}
