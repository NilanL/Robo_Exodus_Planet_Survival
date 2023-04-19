using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class BuildingConstructionPreview : MonoBehaviour
{
    private Camera _camera;
    public GameObject buildingPrefab = null;
    public Material allowed;
    public Material denied;
    private bool canBuild = false;
    private LayerMask fogOfWarLayer;
    private LayerMask groundLayer;
    private Renderer objRenderer;

    private GameObject gm;
    private GameManager gameManger;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.Find("ParentCamera/Main Camera").GetComponent<Camera>();
        fogOfWarLayer = LayerMask.GetMask("FogOfWar");
        groundLayer = LayerMask.GetMask("Ground");
        objRenderer = gameObject.transform.GetChild(0).GetComponent<Renderer>();
        gm = GameObject.Find("GameManager");
        gameManger = gm.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 500f, (~fogOfWarLayer & groundLayer), QueryTriggerInteraction.Collide))
        {
            transform.position = hit.point;

            // Collision to avoid building overlap
            var colliders = Physics.OverlapSphere(transform.position, 9f, ~(fogOfWarLayer & groundLayer))
                                    .Where(x => x.tag.Equals("Building") || x.tag.Equals("Selectable") || x.tag.Equals("Fog_Of_War_Clouds"));

            if (hit.collider.gameObject.tag != "MainBaseBounds" || colliders.Count() != 0)
            {
                if (canBuild)
                {
                    canBuild = false;
                    objRenderer.material = denied;
                }
            }
            else
            {
                if (!canBuild)
                {
                    canBuild = true;
                    objRenderer.material = allowed;
                }
            }
        }

        // Turn building left
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0f, -7.5f, 0f, Space.World);
        }

        // Turn building right
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0f, 7.5f, 0f, Space.World);
        }

        // Quit build mode
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameManger.SetBuildingWarningMessage(false);
            gameManger.SetBuildingMessage(false);

            // Erase preview object
            Destroy(gameObject);
        }

        // Create building
        if (Input.GetMouseButtonDown(0) && canBuild)
        {
            if (buildingPrefab != null)
            {
                switch (buildingPrefab.GetComponent<Building_Name>().buildingName)
                {
                    case BuildingName.BaseDefense:
                        if (verifyRequirement(1000, 0, 0, 0))
                        {
                            gameManger.SetIsDefensesBuildingBuilt();
                            gameManger.Ironite -= 1000;
                        }
                        else
                        {
                            canBuild = false;
                        }
                        break;
                    case BuildingName.TroopCap:
                        if (verifyRequirement(1000, 0, 0, 0))
                        {
                            gameManger.IncrementTroopCapBuildings();
                            gameManger.Ironite -= 1000;
                        }
                        else
                        {
                            canBuild = false;
                        }
                        break;
                    case BuildingName.TroopProd:
                        if (verifyRequirement(1000, 0, 0, 0))
                        {
                            gameManger.IncrementTroopProdBuildings();
                            gameManger.Ironite -= 1000;
                        }
                        else
                        {
                            canBuild = false;
                        }
                        break;
                    case BuildingName.Turret:
                        if (verifyRequirement(1000, 0, 0, 0))
                        {
                            gameManger.IncrementTurretCount();
                            gameManger.Ironite -= 1000;
                        }
                        else
                        {
                            canBuild = false;
                        }
                        break;
                }


                if (canBuild)
                {
                    Instantiate(buildingPrefab, transform.position, transform.rotation);

                    // Erase preview object
                    Destroy(gameObject);
                }
                else
                {
                    ShowWarning();
                }
            }
            else
            {
                ShowWarning();
            }
        }
    }

    public bool verifyRequirement(int neededIronite, int neededZorium, int neededAurarium, int neededBloodstone)
    {
        if (gameManger.Ironite < neededIronite)
            return false;

        if (gameManger.Zorium < neededZorium)
            return false;

        if (gameManger.Aurarium < neededAurarium)
            return false;

        if (gameManger.BloodStone < neededBloodstone)
            return false;

        return true;
    }

    public void ShowWarning()
    {
        canBuild = false;
        objRenderer.material = denied;
        StartCoroutine(ShowWarningMessage());
    }

    // Assigns building prefab
    public void InitializePreview(GameObject prefab)
    {
        buildingPrefab = prefab;
    }

    // Shows resource warning message for 5 seconds
    IEnumerator ShowWarningMessage()
    {
        gameManger.SetBuildingWarningMessage(true);
        yield return new WaitForSeconds(5);
        gameManger.SetBuildingWarningMessage(false);
    }

    void OnDrawGizmos()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 10f);
    }
}
