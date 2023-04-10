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
    FORCombatTest test;

    private GameObject UI;

    public bool IsWallBuilt { get; private set; } = false;
    public bool IsDefensesBuildingCreated { get; private set; } = false;
    public int ShipUpgradeLevel { get; private set; } = 0;
    public int TroopCapBuildingCount { get; private set; } = 0;
    public int TroopProdBuildingCount { get; private set; } = 0;
    public int TurretCount { get; private set; } = 0;
    public int MaxUnitCount { get; private set; } = 20;

    //This is just for the test
    private bool spawn = true;
    int count = 5;

    // Start is called before the first frame update
    void Start()
    {
        test = GetComponent<FORCombatTest>();
        UI = GameObject.Find("UI");
    }

    // Update is called once per frame
    void Update()
    {

        //This is just for the Test for getting balance for the first enemies
        if(spawn)
            StartCoroutine(Spawn());


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

    public void GetMinerStats()
    {

    }

    public void SetIsWallBuilt()
    {
        IsWallBuilt = true;

        var buildingButton = UI.transform.Find("HUD/Top Bar/Building Button").gameObject;
        buildingButton.GetComponent<Button>().interactable = true;

        var wallBuildButton = UI.transform.Find("Troop Creation Window/Build Defenses").gameObject;
        wallBuildButton.GetComponent<Button>().interactable = false;
    }

    public void SetIsDefensesBuildingBuilt()
    {
        IsDefensesBuildingCreated = true;

        var defensesBuildButton = UI.transform.Find("Building Creation Window/Build Defenses Computer").gameObject;
        defensesBuildButton.GetComponent<Button>().interactable = false;

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
