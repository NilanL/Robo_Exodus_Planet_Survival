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

            var colliders = Physics.OverlapSphere(transform.position, 9f, ~(fogOfWarLayer & groundLayer)).Where(x => x.tag.Equals("Building") || x.tag.Equals("Selectable"));
            //Debug.Log(colliders.Count());
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
            transform.Rotate(0f, -3f, 0f, Space.World);
        }

        // Turn building right
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0f, 3f, 0f, Space.World);
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
            if (buildingPrefab != null && gameManger.Ironite >= 1000)
            {
                switch (buildingPrefab.GetComponent<Building_Name>().buildingName)
                {
                    case BuildingName.BaseDefense:
                        gameManger.SetIsDefensesBuildingBuilt();
                        gameManger.Ironite -= 1000;
                        break;
                    case BuildingName.TroopCap:
                        gameManger.IncrementTroopCapBuildings();
                        gameManger.Ironite -= 1000;
                        break;
                    case BuildingName.TroopProd:
                        gameManger.IncrementTroopProdBuildings();
                        gameManger.Ironite -= 1000;
                        break;
                    case BuildingName.Turret:
                        gameManger.IncrementTurretCount();
                        gameManger.Ironite -= 1000;
                        break;
                }

                Instantiate(buildingPrefab, transform.position, transform.rotation);

                // Erase preview object
                Destroy(gameObject);
            }
            else
            {
                canBuild = false;
                objRenderer.material = denied;
                StartCoroutine(ShowWarningMessage());
            }
        }
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
