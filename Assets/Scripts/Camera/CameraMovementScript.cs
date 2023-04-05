using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    float speed = .03f;
    float zoomSpeed = 10f;
    float rotateSpeed = .1f;

    float maxHeight = 55f;
    float minHeight = 20f;

    float maxlenght = 500f;
    float minlenght = -500f;

    float maxwidth = 500f;
    float minwidth = -500f;


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

    // Start is called before the first frame update
    void Start()
    {
        myCam = Camera.main;
        selectedGameObjects = new List<GameObject>();
        selectedGameObject = GameObject.FindGameObjectWithTag("Ground");
        startP = Vector2.zero;
        endP = Vector2.zero;
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
        if (Vector3.Distance(heightcube.transform.position, transform.position) < 20)
        {
            maxHeight += 1;
            minHeight += 1;
        }
        if (Vector3.Distance(heightcube.transform.position, transform.position) > 55)
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
        /*
        if ((hsp > 0) && (transform.position.x >= maxlenght))
        {
            hsp = 0;
        }
        else if ((hsp < 0) && (transform.position.x <= minlenght))
        {
            hsp = 0;
        }
        if ((transform.position.x + scrollSP) > maxlenght)
        {
            hsp = maxlenght - transform.position.x;
        }
        else if ((transform.position.x + scrollSP) < minlenght)
        {
            hsp = minlenght - transform.position.x;
        }
        if ((hsp > 0) && (transform.position.z >= maxwidth))
        {
            vsp = 0;
        }
        else if ((hsp < 0) && (transform.position.z <= minwidth))
        {
            vsp = 0;
        }
        if ((transform.position.z + scrollSP) > maxwidth)
        {
            vsp = maxlenght - transform.position.z;
        }
        else if ((transform.position.z + scrollSP) < minwidth)
        {
            vsp = minlenght - transform.position.z;
        }
        */

        //Calculates the speed for the camera
        Vector3 moveVertical = new Vector3(0, scrollSP, 0);
        Vector3 moveLateral = hsp * transform.right;
        Vector3 moveFoward = transform.forward;
        moveFoward.y = 0;
        moveFoward.Normalize();
        moveFoward *= vsp;

        Debug.Log(moveFoward + " " + moveLateral + " " +  moveVertical);

        //Moves the Camera
        Vector3 move = moveVertical + moveLateral + moveFoward;



        transform.position += move;

        if ((transform.position.x) > maxlenght)
        {
            transform.position = new Vector3(maxlenght, transform.position.y, transform.position.z);
        }
        if ((transform.position.x) < minlenght)
        {
            transform.position = new Vector3(minlenght, transform.position.y, transform.position.z);
        }
        if ((transform.position.z) > maxwidth)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, maxwidth);
        }
        if ((transform.position.z) < minwidth)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, minwidth);
        }


        GetCameraRotation();

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