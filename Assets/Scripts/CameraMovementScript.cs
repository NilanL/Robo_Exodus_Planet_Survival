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

    Vector2 pos1;
    Vector2 pos2;

    public GameObject selectedGameObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Adds a go faster key
        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = 0.1f;
            zoomSpeed = 15f;
        }
        else
        {
            speed = .03f;
            zoomSpeed = 10f;
        }

        float hsp = transform.position.y * speed * Input.GetAxis("Horizontal");
        float vsp = transform.position.y * speed * Input.GetAxis("Vertical");
        float scrollSP = Mathf.Log(transform.position.y)  * -zoomSpeed * Input.GetAxis("Mouse ScrollWheel");

        //Limits the height for the camera we can hard code this
        if((scrollSP > 0) && (transform.position.y >= maxHeight))
        {
            scrollSP = 0;
        }
        else if((scrollSP < 0) && (transform.position.y <= minHeight))
        {
            scrollSP = 0;
        }
        if ((transform.position.y +scrollSP) > maxHeight)
        {
            scrollSP = maxHeight - transform.position.y;
        }
        else if((transform.position.y + scrollSP) < minHeight)
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

        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log(hit.collider.gameObject.name);
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
                    foreach(var go in gol)
                    {
                        if (go)
                            go.gameObject.SetActive(false);
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
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
        }

    }

    void GetCameraRotation()
    {
        if(Input.GetMouseButtonDown(2))
        {
            pos1 = Input.mousePosition;
        }

        if(Input.GetMouseButton(2))
        {
            pos2 = Input.mousePosition;

            float dx = (pos2 - pos1).x * rotateSpeed;

            transform.rotation *= Quaternion.Euler(new Vector3(0, dx, 0));

            pos1 = pos2;
        }
    }
}
