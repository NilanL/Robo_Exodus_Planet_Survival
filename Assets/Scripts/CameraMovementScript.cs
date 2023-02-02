using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    float speed = .06f;
    float zoonSpeed = 10f;
    float rotateSpeed;

    float maxHeight = 12f;
    float midHeight = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float hsp = speed * Input.GetAxis("Horizontal");
        float vsp = speed * Input.GetAxis("Vertical");
        float scrollSP = -zoonSpeed * Input.GetAxis("Mouse ScrollWheel");

        Vector3 moveVertical = new Vector3(0, scrollSP, 0);
        Vector3 moveLateral = hsp * transform.right;
        Vector3 moveFoward = transform.forward;
        moveFoward.y = 0;
        moveFoward.Normalize();
        moveFoward *= vsp;

        Vector3 move = moveVertical + moveLateral + moveFoward;

        transform.position += move;

    }
}
