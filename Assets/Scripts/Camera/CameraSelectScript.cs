using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class CameraSelectScript : MonoBehaviour
{
    [SerializeField]
    public RectTransform boxvisual;

    Rect selectionBox;

    Vector2 startP;
    Vector2 endP;

    Vector2 pos1;
    Vector2 pos2;

    Camera myCam;

    GameObject selectedArea;
    public GameObject selectedGameObject;
    public List<GameObject> selectedGameObjects;

    private LayerMask fogOfWarLayer;

    bool keydown = false;

    void Start()
    {
        myCam = Camera.main;
        selectedGameObjects = new List<GameObject>();
        selectedGameObject = GameObject.FindGameObjectWithTag("Ground");
        selectedArea = GameObject.FindGameObjectWithTag("Area_Selected");
        fogOfWarLayer = LayerMask.GetMask("FogOfWar");
        startP = Vector2.zero;
        endP = Vector2.zero;
        DrawVisual();
    }

    void Update()
    {

        if (Input.GetKeyDown("left shift"))
        {
            keydown = true;
        }
        if (Input.GetKeyUp("left shift"))
        {
            keydown = false;
        }

        if (Input.GetMouseButtonDown(0) && !keydown)
        {
            startP = Input.mousePosition;
            selectionBox = new Rect();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, ~fogOfWarLayer))
            {
                var hitag = hit.collider.gameObject.tag;

                switch(hitag)
                {
                    case "Building":
                        BuildingSelect(hit);
                        break;
                    case "Ground":
                        GroundSelect(hit);
                        break;
                    case "Selectable":
                        UnitSelect(hit);
                        break;

                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            endP = Input.mousePosition;
            DrawVisual();
            DrawSelection();
        }

        if (Input.GetMouseButtonUp(0))
        {
            SelectUnits();
            startP = Vector2.zero;
            endP = Vector2.zero;
            DrawVisual();
        }

        /*if (Input.GetMouseButtonDown(0) && keydown)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, ~fogOfWarLayer))
            {
                Debug.Log(hit.collider.gameObject.name);
                selectedGameObject = hit.collider.gameObject;
                if (selectedGameObject)
                    selectedGameObjects.Add(selectedGameObject);
                var hb = selectedGameObject.transform.Find("Healthbar Canvas");

                if (hb)
                    hb.gameObject.SetActive(true);
                else
                {
                    var gol = GameObject.FindGameObjectsWithTag("Health_Bar");
                    foreach (var go in gol)
                    {
                        if (go)
                            go.gameObject.SetActive(false);
                    }
                }

                selectedGameObject = null;
            }
        }*/

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, ~fogOfWarLayer))
            {
                if (!(selectedGameObject is null))
                {
                    var unit = selectedGameObject.gameObject.GetComponent<Unit_Name>().unit_Name;
                    if (selectedGameObject.tag == "Selectable")
                    {
                        if (hit.collider.gameObject.GetComponent<ResourceType>())
                        {
                            
                            var hb = hit.collider.gameObject.transform.Find("Healthbar Canvas");
                            if (hb)
                                hb.gameObject.SetActive(true);
                            if (unit == Unit_Names.Miner)
                            {
                                selectedGameObject.gameObject.GetComponent<Robot_Miner_Controller_Mouse>().target = hit.transform;
                                selectedGameObject.gameObject.GetComponent<Robot_Miner_Controller_Mouse>().IsMiningMove();
                                selectedGameObject.gameObject.GetComponent<TaskManager>().setTarget(hit.collider.gameObject);
                            }
                            else
                            {

                            }
                        }
                        else if (hit.collider.gameObject.GetComponent<Unit_Name>())
                        {
                            var hb = hit.collider.gameObject.transform.Find("Healthbar Canvas");
                            if (hb)
                                hb.gameObject.SetActive(true);
                            switch(unit)
                            {
                                case Unit_Names.Miner:
                                    selectedGameObject.gameObject.GetComponent<Robot_Miner_Controller_Mouse>().target = hit.transform;
                                    selectedGameObject.gameObject.GetComponent<Robot_Miner_Controller_Mouse>().IsMiningMove();
                                    selectedGameObject.gameObject.GetComponent<TaskManager>().setTarget(hit.collider.gameObject);
                                    break;
                                case Unit_Names.Robot_Melee:
                                    selectedGameObject.gameObject.GetComponent<MovementScript>().target = hit.transform;
                                    selectedGameObject.gameObject.GetComponent<MovementScript>().IsMiningMove();
                                    selectedGameObject.gameObject.GetComponent<TaskManager>().setTarget(hit.collider.gameObject);
                                    break;
                                case Unit_Names.Robot_Ranged:
                                    selectedGameObject.gameObject.GetComponent<MovementScript>().target = hit.transform;
                                    selectedGameObject.gameObject.GetComponent<MovementScript>().IsMiningMove();
                                    selectedGameObject.gameObject.GetComponent<TaskManager>().setTarget(hit.collider.gameObject);
                                    break;
                            }
                        }
                        else
                        {
                            switch(unit)
                            {
                                case Unit_Names.Miner:
                                    selectedGameObject.gameObject.GetComponent<Robot_Miner_Controller_Mouse>().IsSelected();
                                    selectedGameObject.gameObject.GetComponent<Robot_Miner_Controller_Mouse>().Movement(ray);
                                    selectedArea.transform.position = hit.point;
                                    break;
                                case Unit_Names.Robot_Melee:
                                    selectedGameObject.gameObject.GetComponent<MovementScript>().IsSelected();
                                    selectedGameObject.gameObject.GetComponent<MovementScript>().Movement(ray);
                                    break;
                                case Unit_Names.Robot_Ranged:
                                    selectedGameObject.gameObject.GetComponent<MovementScript>().IsSelected();
                                    selectedGameObject.gameObject.GetComponent<MovementScript>().Movement(ray);
                                    break;
                            }
                            Debug.Log(hit.collider.transform.position);

                        }
                    }
                }
                else
                {
                    
                    if (hit.collider.gameObject.GetComponent<ResourceType>())
                    {
                        var hb = hit.collider.gameObject.transform.Find("Healthbar Canvas");
                        if (hb)
                            hb.gameObject.SetActive(true);
                        foreach (var sgo in selectedGameObjects)
                        {
                            var unit = sgo.gameObject.GetComponent<Unit_Name>().unit_Name;
                            if (unit == Unit_Names.Miner)
                            {
                                sgo.gameObject.GetComponent<Robot_Miner_Controller_Mouse>().target = hit.transform;
                                sgo.gameObject.GetComponent<Robot_Miner_Controller_Mouse>().IsMiningMove();
                                sgo.gameObject.GetComponent<TaskManager>().setTarget(hit.collider.gameObject);
                            }
                        }

                    }
                    else if (hit.collider.gameObject.GetComponent<Unit_Name>())
                    {
                        var hb = hit.collider.gameObject.transform.Find("Healthbar Canvas");
                        if (hb)
                            hb.gameObject.SetActive(true);
                        foreach (var sgo in selectedGameObjects)
                        {
                            var unit = sgo.gameObject.GetComponent<Unit_Name>().unit_Name;
                            switch (unit)
                            {
                                case Unit_Names.Miner:
                                    sgo.gameObject.GetComponent<Robot_Miner_Controller_Mouse>().target = hit.transform;
                                    sgo.gameObject.GetComponent<Robot_Miner_Controller_Mouse>().IsMiningMove();
                                    sgo.gameObject.GetComponent<TaskManager>().setTarget(hit.collider.gameObject);
                                    break;
                                case Unit_Names.Robot_Melee:
                                    sgo.gameObject.GetComponent<MovementScript>().target = hit.transform;
                                    sgo.gameObject.GetComponent<MovementScript>().IsMiningMove();
                                    sgo.gameObject.GetComponent<TaskManager>().setTarget(hit.collider.gameObject);
                                    break;
                                case Unit_Names.Robot_Ranged:
                                    sgo.gameObject.GetComponent<MovementScript>().target = hit.transform;
                                    sgo.gameObject.GetComponent<MovementScript>().IsMiningMove();
                                    sgo.gameObject.GetComponent<TaskManager>().setTarget(hit.collider.gameObject);
                                    break;

                            }
                        }

                    }
                    else
                    {
                        Debug.Log(hit.collider.gameObject.name);
                        foreach (var sgo in selectedGameObjects)
                        {
                            var unit = sgo.gameObject.GetComponent<Unit_Name>().unit_Name;
                            switch (unit)
                            {
                                case Unit_Names.Miner:
                                    sgo.gameObject.GetComponent<Robot_Miner_Controller_Mouse>().IsSelected();
                                    sgo.gameObject.GetComponent<Robot_Miner_Controller_Mouse>().Movement(ray);
                                    break;
                                case Unit_Names.Robot_Melee:
                                    sgo.gameObject.GetComponent<MovementScript>().IsSelected();
                                    sgo.gameObject.GetComponent<MovementScript>().Movement(ray);
                                    break;
                                case Unit_Names.Robot_Ranged:
                                    sgo.gameObject.GetComponent<MovementScript>().IsSelected();
                                    sgo.gameObject.GetComponent<MovementScript>().Movement(ray);
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }



    void DrawVisual()
    {
        Vector2 boxstart = startP;
        Vector2 boxEnd = endP;

        Vector2 boxc = (boxstart + boxEnd) / 2;
        boxvisual.position = boxc;

        Vector2 boxSize = new Vector2(Mathf.Abs(boxstart.x - boxEnd.x), Mathf.Abs(boxstart.y - boxEnd.y));

        boxvisual.sizeDelta = boxSize;
    }

    void DrawSelection()
    {
        if (Input.mousePosition.x < startP.x)
        {
            //dragging to the left
            selectionBox.xMin = Input.mousePosition.x;
            selectionBox.xMax = startP.x;
        }
        else
        {
            //dragging to the right
            selectionBox.xMin = startP.x;
            selectionBox.xMax = Input.mousePosition.x;

        }

        if (Input.mousePosition.y < startP.y)
        {
            //dragging it down
            selectionBox.yMin = Input.mousePosition.y;
            selectionBox.yMax = startP.y;
        }
        else
        {
            //dragging it up
            selectionBox.yMin = startP.y;
            selectionBox.yMax = Input.mousePosition.y;
        }
    }

    void SelectUnits()
    {
        selectedGameObjects.Clear();
        var units = GameObject.FindGameObjectsWithTag("Selectable");
        foreach (var unit in units)
        {
            if (selectionBox.Contains(myCam.WorldToScreenPoint(unit.transform.position)))
            {
                selectedGameObjects.Add(unit);
                var hb = unit.transform.Find("Healthbar Canvas");

                if (hb)
                    hb.gameObject.SetActive(true);

                selectedGameObject = null;
            }

        }
    }

    void BuildingSelect(RaycastHit hit)
    {
        Debug.Log(hit.collider.gameObject.tag);
        var UI = GameObject.Find("UI");
        var mm = UI.transform.Find("Troop Creation Window");
        if (mm)
            mm.gameObject.SetActive(true);
        selectedGameObjects.Clear();
        selectedGameObject = hit.collider.gameObject;
        var hb = selectedGameObject.transform.Find("Healthbar Canvas");
        var gold = GameObject.FindGameObjectsWithTag("Health_Bar");

        foreach (var go in gold)
        {
            if (go)
                go.gameObject.SetActive(false);
        }

        if (hb)
            hb.gameObject.SetActive(true);

        else
        {
            var gol = GameObject.FindGameObjectsWithTag("Health_Bar");
            foreach (var go in gol)
            {
                if (go)
                    go.gameObject.SetActive(false);
            }
        }
    }

    void GroundSelect(RaycastHit hit)
    {
        selectedGameObjects.Clear();
        selectedGameObject = hit.collider.gameObject;
        var hb = selectedGameObject.transform.Find("Healthbar Canvas");
        var gold = GameObject.FindGameObjectsWithTag("Health_Bar");

        foreach (var go in gold)
        {
            if (go)
                go.gameObject.SetActive(false);
        }

        if (hb)
            hb.gameObject.SetActive(true);

        else
        {
            var gol = GameObject.FindGameObjectsWithTag("Health_Bar");
            foreach (var go in gol)
            {
                if (go)
                    go.gameObject.SetActive(false);
            }
        }
    }

    void UnitSelect(RaycastHit hit)
    {
        selectedGameObjects.Clear();
        selectedGameObject = hit.collider.gameObject;
        var UI = GameObject.Find("Make Miner");
        if (UI)
            UI.SetActive(false);
        var hb = selectedGameObject.transform.Find("Healthbar Canvas");
        var gold = GameObject.FindGameObjectsWithTag("Health_Bar");

        foreach (var go in gold)
        {
            if (go)
                go.gameObject.SetActive(false);
        }

        if (hb)
            hb.gameObject.SetActive(true);

        else
        {
            var gol = GameObject.FindGameObjectsWithTag("Health_Bar");
            foreach (var go in gol)
            {
                if (go)
                    go.gameObject.SetActive(false);
            }
        }
    }

    public void Removeselected(GameObject obj)
    {
        if(!(selectedGameObject is null))
        {
            selectedGameObject = null;
        }
        else
        {
            selectedGameObjects.Remove(obj);
        }
    }
}


