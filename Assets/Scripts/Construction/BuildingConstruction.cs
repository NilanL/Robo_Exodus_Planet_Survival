using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{
    public GameObject unitProdPreviewPrefab;
    public GameObject defensesPreviewPrefab;
    public GameObject troopCapPreviewPrefab;
    public GameObject turretPreviewPrefab;

    public GameObject unitProdPrefab;
    public GameObject defensesPrefab;
    public GameObject troopCapPrefab;
    public GameObject turretPrefab;

    private void Start()
    {
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

    public void CreateTurretBuilding()
    {
        var building = Instantiate(turretPreviewPrefab);
        building.GetComponent<BuildingConstructionPreview>().InitializePreview(turretPrefab);
    }
}
