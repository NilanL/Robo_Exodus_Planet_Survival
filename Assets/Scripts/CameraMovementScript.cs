using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    float speed = .03f;
    float zoomSpeed = 10f;
    float rotateSpeed = .1f;

    float maxHeight = 32f;
    float minHeight = 12f;

    bool keydown = false;

    [SerializeField]
    public RectTransform boxvisual;

    Rect selectionBox;

    Vector2 startP;
    Vector2 endP;

    Vector2 pos1;
    Vector2 pos2;

    Camera myCam;

    public GameObject selectedGameObject;
    public List<GameObject> selectedGameObjects;

    private int ignoreLayer;

    // Start is called before the first frame update
    void Start()
    {
        myCam = Camera.main;
        selectedGameObjects = new List<GameObject>();
        selectedGameObject = GameObject.FindGameObjectWithTag("Ground");
        startP = Vector2.zero;
        endP = Vector2.zero;
        ignoreLayer = 11; // Fog Of War Index
        DrawVisual();
    }

    // Update is called once per frame
    void Update()
    {
        //Adds a go faster key
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 0.1f;
            zoomSpeed = 15f;
        }
        else
        {
            speed = .03f;
            zoomSpeed = 10f;
        }
        //minHeight = 12f;
        //maxHeight = 32f;
        float hsp = transform.position.y * speed * Input.GetAxis("Horizontal");
        float vsp = transform.position.y * speed * Input.GetAxis("Vertical");
        var heightcube = GameObject.Find("Height_Check");
        float scrollSP = Mathf.Log(transform.position.y) * -zoomSpeed * Input.GetAxis("Mouse ScrollWheel");
        var floor = GameObject.Find("Terrain_0_0_6c7ab805-d6b8-439a-8805-7f652d662845");
        if (Vector3.Distance(heightcube.transform.position, transform.position) < 10)
        {
            maxHeight += 1;
            minHeight += 1;
        }
        if (Vector3.Distance(heightcube.transform.position, transform.position) > 33)
        {
            maxHeight -= 1;
            minHeight -= 1;
        }

        //Limits the height for the camera we can hard code this
        if ((scrollSP > 0) && (transform.position.y >= maxHeight))
        {
            scrollSP = 0;
        }
        else if ((scrollSP < 0) && (transform.position.y <= minHeight))
        {
            scrollSP = 0;
        }
        if ((transform.position.y + scrollSP) > maxHeight)
        {
            scrollSP = maxHeight - transform.position.y;
        }
        else if ((transform.position.y + scrollSP) < minHeight)
        {
            scrollSP = minHeight - transform.position.y;
        }

        //Calculates the speed for the camera
        Vector3 moveVertical = new Vector3(0, scrollSP, 0);
        Vector3 moveLateral = hsp * transform.right;
        Vector3 moveFoward = transform.forward;
        moveFoward.y = 0;
        moveFoward.Normalize();
        moveFoward *= vsp;

        //Moves the Camera
        Vector3 move = moveVertical + moveLateral + moveFoward;

        transform.position += move;

        GetCameraRotation();
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
            if (Physics.Raycast(ray, out hit, 100, ~(1 << ignoreLayer)))
            {
                if (hit.collider.gameObject.tag == "Building")
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
                else if (hit.collider.gameObject.tag == "Ground")
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
                else
                {
                    //Debug.Log(hit.collider.gameObject.name);
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

        if (Input.GetMouseButtonDown(0) && keydown)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, ~(1 << ignoreLayer)))
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
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, ~(1 << ignoreLayer)))
            {
                if (!(selectedGameObject is null))
                {
                    if (selectedGameObject.tag == "Selectable")
                    {
                        if (hit.collider.gameObject.GetComponent<ResourceType>())
                        {
                            var hb = hit.collider.gameObject.transform.Find("Healthbar Canvas");
                            if (hb)
                                hb.gameObject.SetActive(true);
                            selectedGameObject.gameObject.GetComponent<Robot_Miner_Controller_Mouse>().target = hit.transform;
                            selectedGameObject.gameObject.GetComponent<Robot_Miner_Controller_Mouse>().IsMiningMove();
                            selectedGameObject.gameObject.GetComponent<TaskManager>().setTarget(hit.collider.gameObject);
                        }
                        else if (hit.collider.gameObject.GetComponent<Unit_Name>())
                        {
                            var hb = hit.collider.gameObject.transform.Find("Healthbar Canvas");
                            if (hb)
                                hb.gameObject.SetActive(true);
                            selectedGameObject.gameObject.GetComponent<Robot_Miner_Controller_Mouse>().target = hit.transform;
                            selectedGameObject.gameObject.GetComponent<Robot_Miner_Controller_Mouse>().IsMiningMove();
                            selectedGameObject.gameObject.GetComponent<TaskManager>().setTarget(hit.collider.gameObject);
                        }
                        else
                        {
                            Debug.Log(hit.collider.gameObject.name);
                            selectedGameObject.gameObject.GetComponent<Robot_Miner_Controller_Mouse>().IsSelected();
                            selectedGameObject.gameObject.GetComponent<Robot_Miner_Controller_Mouse>().Movement(ray);
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
                            sgo.gameObject.GetComponent<Robot_Miner_Controller_Mouse>().target = hit.transform;
                            sgo.gameObject.GetComponent<Robot_Miner_Controller_Mouse>().IsMiningMove();
                            sgo.gameObject.GetComponent<TaskManager>().setTarget(hit.collider.gameObject);
                        }

                    }
                    else if (hit.collider.gameObject.GetComponent<Unit_Name>())
                    {
                        var hb = hit.collider.gameObject.transform.Find("Healthbar Canvas");
                        if (hb)
                            hb.gameObject.SetActive(true);
                        foreach (var sgo in selectedGameObjects)
                        {
                            sgo.gameObject.GetComponent<Robot_Miner_Controller_Mouse>().target = hit.transform;
                            sgo.gameObject.GetComponent<Robot_Miner_Controller_Mouse>().IsMiningMove();
                            sgo.gameObject.GetComponent<TaskManager>().setTarget(hit.collider.gameObject);
                        }

                    }
                    else
                    {
                        Debug.Log(hit.collider.gameObject.name + " " + hit.collider.gameObject.layer);
                        foreach (var sgo in selectedGameObjects)
                        {
                            sgo.gameObject.GetComponent<Robot_Miner_Controller_Mouse>().IsSelected();
                            sgo.gameObject.GetComponent<Robot_Miner_Controller_Mouse>().Movement(ray);
                        }
                    }
                }
            }
        }

    }

    void GetCameraRotation()
    {
        if (Input.GetMouseButtonDown(2))
        {
            pos1 = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            pos2 = Input.mousePosition;

            float dx = (pos2 - pos1).x * rotateSpeed;

            transform.rotation *= Quaternion.Euler(new Vector3(0, dx, 0));

            pos1 = pos2;
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
}