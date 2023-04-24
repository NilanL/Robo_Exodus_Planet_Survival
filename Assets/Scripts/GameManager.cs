using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{

    public int Ironite;
    public int BloodStone;
    public int Aurarium;
    public int Zorium;
    public int Unit_count;
    public int CoglingRep;
    public int SleemasiRep;
    public int GraxxianRep;
    FORCombatTest test;

    private GameObject UI;
    private GameObject levelLoader;
    public GameObject[] enemies { get; set; }

    public bool IsFoliageCleared { get; private set; } = false;
    public bool IsWallBuilt { get; private set; } = false;
    public bool IsDefensesBuildingCreated { get; private set; } = false;

    public bool IsReactorRepaired {get; private set;} = false;
    public bool IsComputerRepaired { get; private set; } = false;
    public bool IsPlatesRepaired { get; private set; } = false;

    public int ShipUpgradeLevel { get; private set; } = 0;
    public int TroopCapBuildingCount { get; private set; } = 0;
    public int TroopProdBuildingCount { get; private set; } = 0;
    public int TurretCount { get; private set; } = 0;
    public int MaxUnitCount { get; private set; } = 20;

    public int ModulesRepairedCount { get; private set; } = 0;

    public GameObject ActiveTurret { get; private set; } = null;
    public GameObject ActiveWall { get; private set; } = null;
    public GameObject ActiveGate { get; private set; } = null;
    public GameObject ActiveTroopProd { get; private set; } = null;

    private float nextActionTime = 0.0f;
    private float period = 0.5f;

    //This is just for the test
    private bool spawn = true;
    int count = 5;

    // Start is called before the first frame update
    void Start()
    {
        test = GetComponent<FORCombatTest>();
        UI = GameObject.Find("UI");
        levelLoader = GameObject.Find("LevelLoader");
    }

    // Update is called once per frame
    void Update()
    {

        //This is just for the Test for getting balance for the first enemies
        if (spawn)
            StartCoroutine(Spawn());

        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            var coglings = GameObject.FindGameObjectsWithTag("Cogling");
            var en = GameObject.FindGameObjectsWithTag("Enemy");
            enemies = en.Union(coglings).ToArray();
        }
    }

    IEnumerator Spawn()
    {
        spawn = false;
        yield return new WaitForSeconds(120);
        test.ChooseUnits(count);
        count += 5;
        test.SetTarget();
        spawn = true;
    }

    public void SetActiveTurretWindow(GameObject turret)
    {
        ActiveTurret = turret;

        var window = UI.transform.Find("Building Windows/Turret Window").gameObject;
        var health = window.transform.Find("Turret Health").gameObject;

        health.GetComponent<Text>().text = "Health: (" + turret.GetComponent<Unit_Health>().currentHealth + "/" + turret.GetComponent<Unit_Health>().maxHealth + ")";
    }

    public void SetActiveWallWindow(GameObject wall)
    {
        ActiveWall = wall;

        var window = UI.transform.Find("Building Windows/Wall Window").gameObject;
        var health = window.transform.Find("Wall Health").gameObject;

        health.GetComponent<Text>().text = "Health: (" + wall.GetComponent<Unit_Health>().currentHealth + "/" + wall.GetComponent<Unit_Health>().maxHealth + ")";
    }

    public void SetActiveGateWindow(GameObject gate)
    {
        ActiveGate = gate;

        var window = UI.transform.Find("Building Windows/Gate Window").gameObject;
        var health = window.transform.Find("Gate Health").gameObject;

        health.GetComponent<Text>().text = "Health: (" + gate.GetComponent<Unit_Health>().currentHealth + "/" + gate.GetComponent<Unit_Health>().maxHealth + ")";
    }

    public void SetActiveTroopProdWindow(GameObject troopProd)
    {
        ActiveTroopProd = troopProd;
    }

    public void GetMinerStats()
    {

    }

    public void LoseGame()
    {
        levelLoader.GetComponent<LevelLoader>().LoadLoseGame(3);
    }

    public void WinGame()
    {
        levelLoader.GetComponent<LevelLoader>().LoadWinGame(2);
    }

    public void SetIsFoliageCleared()
    {
        IsFoliageCleared = true;

        var button0 = UI.transform.Find("Building Creation Window/Clear Zone Button").gameObject;
        button0.GetComponent<Button>().interactable = false;

        var button1 = UI.transform.Find("Building Creation Window/Build Wall Button").gameObject;
        button1.GetComponent<Button>().interactable = true;

        var button2 = UI.transform.Find("Building Creation Window/Build Troop Coordinator").gameObject;
        button2.GetComponent<Button>().interactable = true;

        var button3 = UI.transform.Find("Building Creation Window/Build Defenses Computer").gameObject;
        button3.GetComponent<Button>().interactable = true;

        var button4 = UI.transform.Find("Building Creation Window/Build Production Center").gameObject;
        button4.GetComponent<Button>().interactable = true;

        var debrisText = UI.transform.Find("Building Creation Window/Alert Screen/Debris Alert").gameObject;
        debrisText.GetComponent<Text>().text = "Debris Cleared. Zone ready for construction.";
        debrisText.GetComponent<Text>().color = new Color(0f, 0f, 0f);
    }

    public void SetIsWallBuilt()
    {
        IsWallBuilt = true;

        var button0 = UI.transform.Find("Building Creation Window/Build Wall Button").gameObject;
        button0.GetComponent<Button>().interactable = false;

        var wallText = UI.transform.Find("Building Creation Window/Alert Screen/Defenses Alert").gameObject;
        wallText.GetComponent<Text>().text = "Defenses Constructed. Zone now defensible.";
        wallText.GetComponent<Text>().color = new Color(0f, 0f, 0f);
        /*
        var buildingButton = UI.transform.Find("HUD/Top Bar/Building Button").gameObject;
        buildingButton.GetComponent<Button>().interactable = true;*/

        /*
        var wallBuildButton = UI.transform.Find("Troop Creation Window/Build Defenses").gameObject;
        wallBuildButton.GetComponent<Button>().interactable = false;*/
    }

    public void SetIsReactorRepaired()
    {
        var button = UI.transform.Find("Module Repair Window/Repair Reactor Button").gameObject;
        button.GetComponent<Button>().interactable = false;

        IsReactorRepaired = true;
        SetModuleRepairedCount();
    }

    public void SetIsComputerRepaired()
    {
        var button = UI.transform.Find("Module Repair Window/Repair Computer Button").gameObject;
        button.GetComponent<Button>().interactable = false;

        IsComputerRepaired = true;
        SetModuleRepairedCount();
    }

    public void SetIsPlatesRepaired()
    {
        var button = UI.transform.Find("Module Repair Window/Repair Plates Button").gameObject;
        button.GetComponent<Button>().interactable = false;

        IsPlatesRepaired = true;
        SetModuleRepairedCount();
    }

    private void SetModuleRepairedCount()
    {
        ModulesRepairedCount += 1;

        if (ModulesRepairedCount == 1)
        {
            GameObject ship1 = GameObject.Find("Robot_Base_Level1");
            ship1.SetActive(false);

            GameObject ship2 = GameObject.Find("Robot_Bases").transform.Find("Robot_Base_Level2").gameObject;
            ship2.SetActive(true);

            /*
            GameObject spawnLocation = GameObject.Find("Robot_Base_Spawn_Location");
            GameObject ship2 = gameObject.GetComponent<Main_Base_Stats>().getLevel2ShipPrefab();
            Instantiate(ship2, spawnLocation.transform.position, spawnLocation.transform.rotation);

            Destroy(ship1);*/
        }
        else if (ModulesRepairedCount >= 3)
        {
            var alertScreen = UI.transform.Find("Module Repair Window/Alert Screen").gameObject;
            alertScreen.SetActive(false);

            var fixedScreen = UI.transform.Find("Module Repair Window/Modules Fixed Screen").gameObject;
            fixedScreen.SetActive(true);

            var launchButton = UI.transform.Find("Building Windows/Main Base Window/Launch Button").gameObject;
            launchButton.SetActive(true);
        }
    }

    public void SetIsDefensesBuildingBuilt()
    {
        IsDefensesBuildingCreated = true;

        var defensesBuildButton = UI.transform.Find("Building Creation Window/Build Defenses Computer").gameObject;
        defensesBuildButton.GetComponent<Button>().interactable = false;

        var defensesCount = UI.transform.Find("Building Creation Window/Defenses Count").gameObject;
        defensesCount.GetComponent<Text>().text = "Current: 1";

        SetBuildingWarningMessage(false);
        SetBuildingMessage(false);
    }

    public void IncrementShipLevel()
    {
        ShipUpgradeLevel += 1;

        // TODO: Change in-game things due to new ship level
    }

    public void IncrementTroopCapBuildings()
    {
        TroopCapBuildingCount += 1;
        MaxUnitCount += 20;

        var troopCapCount = UI.transform.Find("Building Creation Window/Troop Cap Count").gameObject;
        troopCapCount.GetComponent<Text>().text = "Current: " + TroopCapBuildingCount;

        if (TroopCapBuildingCount == 4)
        {
            var troopCapBuildingButton = UI.transform.Find("Building Creation Window/Build Troop Coordinator").gameObject;
            troopCapBuildingButton.GetComponent<Button>().interactable = false;
        }

        SetBuildingWarningMessage(false);
        SetBuildingMessage(false);
        // TODO: Change in-game things due to greater troop cap
    }

    public void IncrementTroopProdBuildings()
    {
        TroopProdBuildingCount += 1;

        SetBuildingWarningMessage(false);
        SetBuildingMessage(false);
        // TODO: Change in-game things due to greater troop cap
    }

    public void IncrementTurretCount()
    {
        TurretCount += 1;

        var turretCountText = UI.transform.Find("Building Windows/Defenses Window/Total Turret Count").gameObject;
        turretCountText.GetComponent<Text>().text = "Active Turrets " + TurretCount + "/" + TurretCount;

        SetBuildingWarningMessage(false);
        SetBuildingMessage(false);
        // TODO: Change in-game things due to increased turret count
    }

    public void SetBuildingMessage(bool val)
    {
        var buildingPreviewMessage = UI.transform.Find("Building Preview Message").gameObject;
        buildingPreviewMessage.SetActive(val);
    }

    public void SetBuildingWarningMessage(bool val)
    {
        var buildingPreviewMessage = UI.transform.Find("Building Preview Message").gameObject;
        var buildingPreviewWarning = buildingPreviewMessage.transform.Find("Preview Warning").gameObject;
        buildingPreviewWarning.SetActive(val);
    }
}
