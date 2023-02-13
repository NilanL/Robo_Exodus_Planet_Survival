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
