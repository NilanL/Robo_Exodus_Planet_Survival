using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{
    public GameObject unitProdPreviewPrefab;
    public GameObject defensesPreviewPrefab;
    public GameObject troopCapPreviewPrefab;

    public GameObject unitProdPrefab;
    public GameObject defensesPrefab;
    public GameObject troopCapPrefab;

    private GameObject UI;

    private void Start()
    {
        UI = GameObject.Find("UI");
    }

    public void CreateUnitProductionBuilding()
    {
        var building = Instantiate(unitProdPreviewPrefab);
        building.GetComponent<BuildingConstructionPreview>().InitializePreview(unitProdPrefab);
    }

    public void CreateDefensesBuilding()
    {
        var building = Instantiate(defensesPreviewPrefab);
        building.GetComponent<BuildingConstructionPreview>().InitializePreview(defensesPrefab);
    }

    public void CreateTroopCapacityBuilding()
    {
        var building = Instantiate(troopCapPreviewPrefab);
        building.GetComponent<BuildingConstructionPreview>().InitializePreview(troopCapPrefab);
    }
}
